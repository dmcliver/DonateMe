using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using jsTree3.Models;

namespace DonateMe.Web.Controllers
{
    public class JsTreeController : Controller
    {
        public ActionResult AjaxDemo()
        {
            return View();
        }

        public JsonResult GetJsTreeData(string id)
        {
            if (id == "#" || id == null)
            {
                JsTreeNode root = BuildRoot();
                return Json(root, JsonRequestBehavior.AllowGet);
            }

            List<JsTreeNode> children = BuildChidren();

            return Json(children, JsonRequestBehavior.AllowGet);
        }

        private static List<JsTreeNode> BuildChidren()
        {
            var children = new List<JsTreeNode>();
            for (int i = 0; i < 5; i++)
            {
                var node = JsTreeNode.NewNode(i.ToString());
//                node.state = new State(IsPrime(i), false, false);

                children.Add(node);
            }
            return children;
        }

        private static JsTreeNode BuildRoot()
        {
            var root = new JsTreeNode() // Create our root node and ensure it is opened
            {
                id = 111.ToString(),
                text = "Root Node",
                //state = new State(true, false, false),
                children = true
            };
            return root;
        }

        static bool IsPrime(int n)
        {
            if (n > 1)
            {
                return Enumerable.Range(1, n)
                                 .Where(x => n % x == 0)
                                 .SequenceEqual(new[] { 1, n });
            }

            return false;
        }
    }
}
