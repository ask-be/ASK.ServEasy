using System;
using System.Collections.Generic;
using Aga.Controls.Tree;
using WeifenLuo.WinFormsUI.Docking;

namespace ASK.ServEasy.Windows.WinForms
{
	public partial class ThreadsDockContent : DockContent
	{
		delegate void ModuleThreadEvenHandler(ModuleThread aThread);

		private TreeModel model = new TreeModel();
		private Dictionary<string, ThreadTreeNode> theThreadTreeNodes = new Dictionary<string, ThreadTreeNode>();

		public ThreadsDockContent()
		{
			InitializeComponent();

			base.Text = "Threads";
		}

		public Module Module { get; set; }

		private void ThreadsDockContent_Load(object sender, EventArgs e)
		{
			if (Module != null)
			{
				ThreadTreeNode node = CreateThreadTreeNode(Module);

				AddSubThreadsTreeNodes(Module.Threads, node);

				model.Nodes.Add(node);
				theTreeViewAdv.Model = model;
				theTreeViewAdv.ExpandAll();
			}
		}

		private void AddSubThreadsTreeNodes(IEnumerable<ModuleThread> iEnumerable, ThreadTreeNode node)
		{
			foreach (ModuleThread thread in iEnumerable)
			{
				ThreadTreeNode newNode = CreateThreadTreeNode(thread);
				AddSubThreadsTreeNodes(thread.Threads, newNode);
				node.Nodes.Add(newNode);
			}
		}

		private ThreadTreeNode CreateThreadTreeNode(ModuleThread thread)
		{
			ThreadTreeNode node = new ThreadTreeNode(thread) {Text = thread.Name, Tag = thread};

			thread.ThreadAdded += thread_ThreadAdded;
			thread.ThreadRemoved += thread_ThreadRemoved;
			thread.StatusMessageChanged += thread_StatusMessageChanged;

			theThreadTreeNodes.Add(thread.Id, node);

			return node;
		}

		void thread_ThreadRemoved(object sender, ModuleThreadEventArgs e)
		{
			ModuleThread deletedThread = e.Thread;
			if (IsHandleCreated)
				BeginInvoke(new ModuleThreadEvenHandler(DeleteThreadNode), deletedThread);
		}

		void thread_ThreadAdded(object sender, ModuleThreadEventArgs e)
		{
			ModuleThread newThread = e.Thread;
			if (IsHandleCreated)
				BeginInvoke(new ModuleThreadEvenHandler(AddThreadNode), newThread);
		}

		void thread_StatusMessageChanged(object sender, ModuleThreadEventArgs e)
		{
			ModuleThread newThread = e.Thread;
			if(IsHandleCreated)
				BeginInvoke(new ModuleThreadEvenHandler(ThreadStatusMessageChanged), newThread);
		}

		private void ThreadStatusMessageChanged(ModuleThread aThread)
		{
			ThreadTreeNode node = theThreadTreeNodes[aThread.Id];
		}

		private void DeleteThreadNode(ModuleThread aDeletedThread)
		{
			if (aDeletedThread.ParentThread != null)
			{
				ThreadTreeNode node = theThreadTreeNodes[aDeletedThread.Id];
				if (node != null && node.Parent != null)
					node.Parent.Nodes.Remove(node);
			}
		}
		private void AddThreadNode(ModuleThread aNewThread)
		{
			if (aNewThread.ParentThread != null)
			{
				ThreadTreeNode node = theThreadTreeNodes[aNewThread.ParentThread.Id];
				if (node != null) node.Nodes.Add(CreateThreadTreeNode(aNewThread));
			}
		}
	}
}
