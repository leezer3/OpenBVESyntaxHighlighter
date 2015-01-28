// Copyright (c) Microsoft Corporation
// All rights reserved

namespace OpenBVESyntax
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;

    [Export(typeof(ITaggerProvider))]
    [ContentType("ook!")]
    [TagType(typeof(OokTokenTag))]
    internal sealed class OokTokenTagProvider : ITaggerProvider
    {

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            return new OokTokenTagger(buffer) as ITagger<T>;
        }
    }

    public class OokTokenTag : ITag 
    {
        public commandTypes type { get; private set; }

        public OokTokenTag(commandTypes type)
        {
            this.type = type;
        }
    }

    internal sealed class OokTokenTagger : ITagger<OokTokenTag>
    {

        ITextBuffer _buffer;
        IDictionary<string, commandTypes> _commandTypes;

        internal OokTokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
            _commandTypes = new Dictionary<string, commandTypes>();
            _commandTypes[".railtype"] = commandTypes.RailType;
            _commandTypes[".railstart"] = commandTypes.RailStart;
            _commandTypes[".rail"] = commandTypes.Rail;
            _commandTypes[".pitch"] = commandTypes.Pitch;
            _commandTypes[".curve"] = commandTypes.Curve;
            _commandTypes[".freeobj"] = commandTypes.FreeObject;
            _commandTypes[".wall"] = commandTypes.Wall;
            _commandTypes[".wallend"] = commandTypes.WallEnd;
            _commandTypes[".dike"] = commandTypes.Dike;
            _commandTypes[".dikeend"] = commandTypes.DikeEnd;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        public IEnumerable<ITagSpan<OokTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {

            foreach (SnapshotSpan curSpan in spans)
            {
                ITextSnapshotLine containingLine = curSpan.Start.GetContainingLine();
                int curLoc = containingLine.Start.Position;
                string[] tokens = containingLine.GetText().ToLower().Split(' ');

                foreach (string ookToken in tokens)
                {
                    if (_commandTypes.ContainsKey(ookToken))
                    {
                        var tokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(curLoc, ookToken.Length));
                        if( tokenSpan.IntersectsWith(curSpan) ) 
                            yield return new TagSpan<OokTokenTag>(tokenSpan, 
                                                                  new OokTokenTag(_commandTypes[ookToken]));
                    }

                    //add an extra char location because of the space
                    curLoc += ookToken.Length + 1;
                }
            }
            
        }
    }
}
