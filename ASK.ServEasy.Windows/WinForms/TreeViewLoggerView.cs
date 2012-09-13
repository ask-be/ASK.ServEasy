using System.Drawing;
using System.Windows.Forms;
using ASK.ServEasy.Windows.Log;

namespace ASK.ServEasy.Windows.WinForms
{
	internal class TreeViewLoggerView : ILoggerView
	{
		public TreeViewLoggerView(TreeView treeView)
		{
			_treeView = treeView;
			_isRoot = true;
		}

		private TreeViewLoggerView(TreeView treeView, TreeNode node)
		{
			_treeView = treeView;
			_node = node;
			_isRoot = false;
		}

		#region ILoggerView Members

		/// <summary>
		/// Clears this view and all child views.
		/// </summary>
		public void Clear()
		{
			if (_isRoot)
			{
				try
				{
					_treeView.BeginUpdate();
					_treeView.Nodes.Clear();
				}
				finally
				{
					_treeView.EndUpdate();
				}
			}
			else
			{
				try
				{
					_node.TreeView.BeginUpdate();
					_node.Nodes.Clear();
				}
				finally
				{
					_node.TreeView.EndUpdate();
				}
			}
		}

		/// <summary>
		/// Adds the new logger view as a child of the current view and returns the new view.
		/// </summary>
		/// <param name="text">The text to initialize the view with.</param>
		/// <param name="logger">The logger that this instance is a view of.</param>
		/// <returns></returns>
		public ILoggerView AddNew(string text, LoggerItem logger)
		{
			// Creating a new node.
			TreeNode node = new TreeNode(text) {Checked = true, Tag = logger};

			if (_isRoot)
			{
				_treeView.Nodes.Add(node);
			}
			else
			{
				_node.Nodes.Add(node);
			}

			node.EnsureVisible();

			return new TreeViewLoggerView(_treeView, node);
		}

		/// <summary>
		/// Gets or sets the text of the view. The text is what is shown to the user.
		/// </summary>
		/// <value>The text.</value>
		public string Text
		{
			get
			{
				return (_isRoot ? "(root)" : _node.Text);
			}
			set
			{
				if (!_isRoot)
				{
					_node.Text = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ILoggerView"/> is shown
		/// as highlighted.
		/// </summary>
		/// <value><c>true</c> if highlighted; otherwise, <c>false</c>.</value>
		public bool Highlight
		{
			get
			{
				return (_isRoot ? false : _node.BackColor == Color.LightBlue);
			}
			set
			{
				if (!_isRoot)
				{
					_node.BackColor = value ? Color.LightBlue : Color.Transparent;
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ILoggerView"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled
		{
			get
			{
				return (_isRoot ? true : _node.Checked);
			}
			set
			{
				if (!_isRoot)
				{
					_node.Checked = value;
				}
			}
		}

		#endregion

		#region Private Members

		private TreeView _treeView;
		private TreeNode _node;
		private bool _isRoot = false;

		#endregion
	}
}
