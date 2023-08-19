using Rhino;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Input.Custom;
using Rhino.UI;
using System.Linq;
using System.Collections.Generic;
using Rhino.Display;
using System.Drawing;

namespace PanelMaker
{
    public class BlockMouseCallback : MouseCallback
    {
        private bool DoubleClickEnabled = false;
        private RhinoObject SelObject;

        

        

        public void BlockContainmentTest(InstanceObject iObj, PickContext picker, ref BoundingBox box)
        {
            if (iObj != null)
            {
                foreach (var subObj in iObj.GetSubObjects())
                {
                    if (!(subObj is InstanceObject))
                    {
                        var geo = subObj.Geometry.Duplicate();
                        var bbox = geo.GetBoundingBox(true);
                        var bl = picker.PickFrustumTest(bbox, out bool _completeContain);
                        if (bl)
                        {
                            box = bbox;
                            break;
                        }
                        else
                        {
                            box = BoundingBox.Unset;
                            continue;
                        }
                    }
                    else
                    {
                        var subIobj = subObj as InstanceObject;
                        BlockContainmentTest(subIobj, picker, ref box);
                    }
                }
            }
        }
        

        public RhinoObject SelectObjectsInObjectGroups(RhinoDoc doc)
        {
            var rs = new GetPoint();
            rs.AcceptNothing(true);
            rs.EnableSnapToCurves(true);
            rs.SetCommandPrompt("Select Object in Block");
            //rs.SetCursor(CursorStyle.Default);
            var result = rs.Get();
            var obj = rs.PointOnObject();
            if (obj != null)
            {
                return obj.Object();
            }
            return null;
        }

    }
}
