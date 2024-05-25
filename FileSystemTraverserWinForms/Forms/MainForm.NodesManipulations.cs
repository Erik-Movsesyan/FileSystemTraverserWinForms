using System.Drawing;
using System.Windows.Forms;

namespace FileSystemTraverserWinForms
{
    public partial class MainForm
    {
        private void ClearNodes(TreeNode parentNode = null)
        {
            if (InvokeRequired)
            {
                Invoke(() => ClearNodes(parentNode));
            }
            else
            {
                if (parentNode != null)
                {
                    parentNode.Nodes.Clear();
                }
                else
                {
                    resultsTree.Nodes.Clear();
                }
            }
        }

        private void AddLoadingNode(TreeNode parentNode)
        {
            if (InvokeRequired)
            {
                Invoke(() => AddLoadingNode(parentNode));
            }
            else
            {
                if (!parentNode.Nodes.ContainsKey("Loading"))
                {
                    var loadingNode = new TreeNode("Loading ...")
                    {
                        Name = "Loading",
                        ForeColor = Color.DeepSkyBlue
                    };
                    parentNode.Nodes.Add(loadingNode);
                }
            }
        }

        private void ModifyLoadingNode(TreeNode parentNode)
        {
            if (InvokeRequired)
            {
                Invoke(() => ModifyLoadingNode(parentNode));
            }
            else
            {
                if (parentNode.Nodes["Loading"] == null)
                    return;

                if (parentNode.Nodes.Count <= 1)
                {
                    var textToDisplay = applyFilterCheckbox.Checked && !IsFilterTextBoxEmpty
                        ? $"No results found satisfying filter [{_currentFilterText}]"
                        : "No results found";

                    parentNode.Nodes["Loading"]!.Text = textToDisplay;
                    parentNode.Nodes["Loading"]!.Name = textToDisplay;
                }
                else
                {
                    parentNode.Nodes.RemoveByKey("Loading");
                }
            }
        }
    }
}
