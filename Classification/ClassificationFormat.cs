using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace OpenBVESyntax
{
    #region Format definition
    /// <summary>
    /// Defines the editor higlighting for railtype commands
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "RailType")]
    [Name("RailType")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class HLTRailType : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "ordinary" classification type
        /// </summary>
        public HLTRailType()
        {
            this.DisplayName = "RailType"; //human readable version of the name
            this.ForegroundColor = Colors.Gold;
        }
    }

    /// <summary>
    /// Defines the editor highlighting for rail commands
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "Rail")]
    [Name("Rail")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class HLTRail : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "ordinary" classification type
        /// </summary>
        public HLTRail()
        {
            this.DisplayName = "Rail"; //human readable version of the name
            this.ForegroundColor = Colors.Gold;
        }
    }

    /// <summary>
    /// Defines the editor highlighting for rail commands
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "RailStart")]
    [Name("RailStart")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class HLTRailStart : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "ordinary" classification type
        /// </summary>
        public HLTRailStart()
        {
            this.DisplayName = "RailStart"; //human readable version of the name
            this.ForegroundColor = Colors.Gold;
        }
    }

    /// <summary>
    /// Defines the editor highlighting for curve commands
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "Curve")]
    [Name("Curve")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class HLTCurve : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "ordinary" classification type
        /// </summary>
        public HLTCurve()
        {
            this.DisplayName = "Curve"; //human readable version of the name
            this.ForegroundColor = Colors.Orange;
        }
    }
    
    /// <summary>
    /// Defines the editor highlighting for pitch commands
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "Pitch")]
    [Name("Pitch")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class HLTPitch : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "ordinary" classification type
        /// </summary>
        public HLTPitch()
        {
            this.DisplayName = "Pitch"; //human readable version of the name
            this.ForegroundColor = Colors.DarkBlue;
        }
    }

    /// <summary>
    /// Defines the editor highlighting for freeobj commands
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "FreeObj")]
    [Name("FreeObj")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class HLTFreeObj : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "ordinary" classification type
        /// </summary>
        public HLTFreeObj()
        {
            this.DisplayName = "FreeObject"; //human readable version of the name
            this.ForegroundColor = Colors.GreenYellow;
        }
    }

    /// <summary>
    /// Defines the editor highlighting for wall commands
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "Wall")]
    [Name("Wall")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class HLTWall : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "ordinary" classification type
        /// </summary>
        public HLTWall()
        {
            this.DisplayName = "Wall"; //human readable version of the name
            this.ForegroundColor = Colors.LightBlue;
        }
    }


    /// <summary>
    /// Defines the editor highlighting for dike commands
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "Dike")]
    [Name("Dike")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class HLTDike : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "ordinary" classification type
        /// </summary>
        public HLTDike()
        {
            this.DisplayName = "Dike"; //human readable version of the name
            this.ForegroundColor = Colors.LightBlue;
        }
    }
    #endregion //Format definition
}
