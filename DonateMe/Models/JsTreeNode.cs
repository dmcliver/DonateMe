using System.Collections.Generic;

namespace DonateMe.Web.Models
{
    public class JsTreeNode
    {
        public string id;
        public string text;
        public string icon;
        //public State state;
        public object children;

        public static JsTreeNode NewNode(string id)
        {
            return new JsTreeNode()
            {
                id = id,
                text = string.Format("Child Node {0}", id),
                children = new List<JsTreeNode>()
            };
        }
    }
    public class State
    {
        public bool opened = false;
        public bool disabled = false;
        public bool selected = false;

        public State(bool Opened, bool Disabled, bool Selected)
        {
            opened = Opened;
            disabled = Disabled;
            selected = Selected;
        }
    }
   
}