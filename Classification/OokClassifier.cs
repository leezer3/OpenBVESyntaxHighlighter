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
    [TagType(typeof(ClassificationTag))]
    internal sealed class OokClassifierProvider : ITaggerProvider
    {

        [Export]
        [Name("ook!")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition OokContentType = null;

        [Export]
        [FileExtension(".ook")]
        [ContentType("ook!")]
        internal static FileExtensionToContentTypeDefinition OokFileType = null;

        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry = null;

        [Import]
        internal IBufferTagAggregatorFactoryService aggregatorFactory = null;

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {

            ITagAggregator<OokTokenTag> ookTagAggregator = 
                                            aggregatorFactory.CreateTagAggregator<OokTokenTag>(buffer);

            return new OokClassifier(buffer, ookTagAggregator, ClassificationTypeRegistry) as ITagger<T>;
        }
    }

    internal sealed class OokClassifier : ITagger<ClassificationTag>
    {
        ITextBuffer _buffer;
        ITagAggregator<OokTokenTag> _aggregator;
        IDictionary<commandTypes, IClassificationType> _ookTypes;

        internal OokClassifier(ITextBuffer buffer, 
                               ITagAggregator<OokTokenTag> ookTagAggregator, 
                               IClassificationTypeRegistryService typeService)
        {
            _buffer = buffer;
            _aggregator = ookTagAggregator;
            _ookTypes = new Dictionary<commandTypes, IClassificationType>();
            _ookTypes[commandTypes.RailType] = typeService.GetClassificationType("railtype");
            _ookTypes[commandTypes.Rail] = typeService.GetClassificationType("rail");
            _ookTypes[commandTypes.RailStart] = typeService.GetClassificationType("railstart");
            _ookTypes[commandTypes.Curve] = typeService.GetClassificationType("curve");
            _ookTypes[commandTypes.Pitch] = typeService.GetClassificationType("pitch");
            _ookTypes[commandTypes.FreeObject] = typeService.GetClassificationType("freeobj");
            _ookTypes[commandTypes.Wall] = typeService.GetClassificationType("wall");
            _ookTypes[commandTypes.WallEnd] = typeService.GetClassificationType("wallend");
            _ookTypes[commandTypes.Dike] = typeService.GetClassificationType("dike");
            _ookTypes[commandTypes.DikeEnd] = typeService.GetClassificationType("dikeend");
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {

            foreach (var tagSpan in this._aggregator.GetTags(spans))
            {
                var tagSpans = tagSpan.Span.GetSpans(spans[0].Snapshot);
                yield return 
                    new TagSpan<ClassificationTag>(tagSpans[0], 
                                                   new ClassificationTag(_ookTypes[tagSpan.Tag.type]));
            }
        }
    }
}
