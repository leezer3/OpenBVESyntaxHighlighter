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
                        //Primary Command Types
                        //Alphabetical Order
                    case commandTypes.Curve:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add(new CurveControl());
                        break;
                    case commandTypes.Dike:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add(new DikeControl() );
                        break;
                    case commandTypes.DikeEnd:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add(new DikeEndControl());
                        break;
                    case commandTypes.Pitch:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add(new PitchControl());
                        break;
                    case commandTypes.FreeObject:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add(new FreeObjControl());
                        break;
                    case commandTypes.Rail:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add(new RailControl());
                        break;
                    case commandTypes.RailStart:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add(new RailStartControl());
                        break;
                    case commandTypes.RailType:       
                    applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                    quickInfoContent.Add(new RailTypeControl() );
                        break;
                    case commandTypes.Wall:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add(new WallControl() );
                        break;
                    case commandTypes.WallEnd:
                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                        quickInfoContent.Add(new WallEndControl());
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

