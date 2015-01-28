using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.VisualStudio.Language.Intellisense;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;
using OpenBVESyntax;

namespace OpenBVESyntax
{
    
    [Export(typeof(IQuickInfoSourceProvider))]
    [ContentType("ook!")]
    [Name("ookQuickInfo")]
    class OokQuickInfoSourceProvider : IQuickInfoSourceProvider
    {

        [Import]
        IBufferTagAggregatorFactoryService aggService = null;

        public IQuickInfoSource TryCreateQuickInfoSource(ITextBuffer textBuffer)
        {
            return new OokQuickInfoSource(textBuffer, aggService.CreateTagAggregator<OokTokenTag>(textBuffer));
        }
    }

    class OokQuickInfoSource : IQuickInfoSource
    {
        private ITagAggregator<OokTokenTag> _aggregator;
        private ITextBuffer _buffer;
        private bool _disposed = false;


        public OokQuickInfoSource(ITextBuffer buffer, ITagAggregator<OokTokenTag> aggregator)
        {
            _aggregator = aggregator;
            _buffer = buffer;
        }

        public void AugmentQuickInfoSession(IQuickInfoSession session, IList<object> quickInfoContent, out ITrackingSpan applicableToSpan)
        {
            applicableToSpan = null;

            if (_disposed)
                throw new ObjectDisposedException("TestQuickInfoSource");

            var triggerPoint = (SnapshotPoint) session.GetTriggerPoint(_buffer.CurrentSnapshot);

            if (triggerPoint == null)
                return;

            foreach (IMappingTagSpan<OokTokenTag> curTag in _aggregator.GetTags(new SnapshotSpan(triggerPoint, triggerPoint)))
            {
                var tagSpan = curTag.Span.GetSpans(_buffer).First();
                switch (curTag.Tag.type)
                {
                    case commandTypes.RailType:
                        
                    applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                    quickInfoContent.Add(new RailTypeControl() );
                        break;
                    case commandTypes.Rail:
                    applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                    quickInfoContent.Add(new RailControl());
                        break;
                    case commandTypes.RailStart:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add(new RailStartControl() );
                        break;
                    case commandTypes.Curve:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add(new CurveControl() );
                        break;
                    case commandTypes.Pitch:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add("Changes the pitch for the player's rail as follows:" + Environment.NewLine +
                                             ".pitch Rate" + Environment.NewLine +
                                             "Rate represents the pitch of the track in thousands. Positive numbers pitch UP and negative numbers pitch DOWN");
                        break;
                    case commandTypes.FreeObject:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add("Places a FreeObject as follows:" + Environment.NewLine +
                                             ".freeobj RailIndex;FreeObjStructureIndex;X;Y;Yaw;Pitch;Roll" + Environment.NewLine +
                                             "RailIndex represents the rail on which the new object is to be placed" + Environment.NewLine +
                                             "FreeObjStructureIndex is a non-negative integer representing the FreeObject to be placed" + Environment.NewLine +
                                             "X represents the horizontal position relative to the rail for the new FreeObject" + Environment.NewLine +
                                             "Y represents the vertical position relative to the rail for the new FreeObject" + Environment.NewLine +
                                             "Yaw represents the angle in degrees by which the object is rotated in the XZ-plane in clock-wise order when viewed from above" + Environment.NewLine +
                                             "Pitch represents the angle in degrees by which the object is rotated in the YZ-plane in clock-wise order when viewed from the left" + Environment.NewLine +
                                             "Roll represents the angle in degrees by which the object is rotated in the XY-plane in clock-wise order when viewed from behind");
                        break;
                    case commandTypes.Wall:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add("Starts or changes a Wall as follows:" + Environment.NewLine +
                                             ".freeobj RailIndex;WallSide;WallIndex" + Environment.NewLine +
                                             "RailIndex represents the rail on which the wall is to be started" + Environment.NewLine +
                                             "WallSide is an integer representing the side on which the wall is to be placed- -1 for left, 0 for both sides and 1 for right" + Environment.NewLine +
                                             "WallIndex is a non-negative integer representing the WallType to use");
                        break;
                    case commandTypes.Dike:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add("Starts or changes a Dike as follows:" + Environment.NewLine +
                                             ".freeobj RailIndex;DikeSide;DikeIndex" + Environment.NewLine +
                                             "RailIndex represents the rail on which the dike is to be started" + Environment.NewLine +
                                             "WallSide is an integer representing the side on which the dike is to be placed- -1 for left, 0 for both sides and 1 for right" + Environment.NewLine +
                                             "WallIndex is a non-negative integer representing the DikeType to use");
                        break;
                }
            }
        }

        public void Dispose()
        {
            _disposed = true;
        }
    }
}

