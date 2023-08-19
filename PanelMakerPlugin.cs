using Rhino;
using Rhino.Collections;
using Rhino.FileIO;
using Rhino.PlugIns;
using Rhino.UI;

namespace PanelMaker
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class PanelMakerPlugin : Rhino.PlugIns.PlugIn
    {
        public PanelMakerPlugin()
        
        {
            Instance = this;
        }

        ///<summary>Gets the only instance of the PanelMakerPlugin plug-in.</summary>
        public static PanelMakerPlugin Instance { get; private set; }

        protected override LoadReturnCode OnLoad(ref string errorMessage)
        {
            // 3. register UI panels
            RegisterPanels();

            // disable rhino double click for custom blocks
            _ = new BlockMouseCallback { Enabled = true };

            // TODO: optimize events, disabled for now because of speed
            

            return base.OnLoad(ref errorMessage);
        }
        private void RegisterPanels()
        {
            Panels.RegisterPanel(this, typeof(PanelMaker.UI.Views.MainView), "Plate Generator", System.Drawing.SystemIcons.Application);
            
            
        }
    }
}