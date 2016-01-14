using System;
using Gtk;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Collections;
using System.Collections.Generic;
using  Newtonsoft.Json;
using CnsTools;
using GLib;
using C5;
using System.Xml.Xsl.Runtime;
using System.Runtime.CompilerServices;

public partial class MainWindow: Gtk.Window
{
	TreeIter SourceIter;
	TreeStore treestore;
    Gtk.Menu TreeViewContextMenu;
    TreeIter oldparenttreeiter = TreeIter.Zero; 

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
      
        CreateTreeView ();
        CreateTreeViewContextMenu ();
        CnsXML.Xml2TreeStore (treestore, "Tree.Xml");
	}

    void CreateTreeView()
    {
         //CreateColumns
        TreeViewColumn col = new TreeViewColumn ();
        CellRendererText cell = new CellRendererText ();
        cell.Editable = true;
        col.Title = "Nodes";
        col.PackStart (cell, true);
        col.AddAttribute (cell, "text", 0);
        treeview1.AppendColumn (col);

        TreeViewColumn col2 = new TreeViewColumn ();
        CellRendererText cell2 = new CellRendererText ();
        cell2.Editable = true;
        col2.Title = "Url";
        col2.PackStart (cell2, true);
        col2.AddAttribute (cell2, "text", 1);
        col2.Visible = false;
        treeview1.AppendColumn (col2);
        treeview1.HeadersVisible = false;

        //Add Store
        treestore = new TreeStore (typeof(string), typeof(string));
        treeview1.Model = treestore;        

        cell.Edited +=  (o, args) => {
            Gtk.TreeIter iter;
            treestore.GetIter (out iter, new Gtk.TreePath (args.Path));

            String newvalue = (String)treestore.GetValue (iter, 0);
            Console.WriteLine (newvalue); 

            treestore.SetValue (iter, 0, args.NewText);
            };

            cell2.Edited += (o, args) => {
                Gtk.TreeIter iter;
                treestore.GetIter (out iter, new Gtk.TreePath (args.Path));

                String newvalue = (String)treestore.GetValue (iter, 1);
                Console.WriteLine (newvalue); 

                treestore.SetValue (iter, 1, args.NewText);
            };

            TargetEntry[] ten = new TargetEntry[]{ new TargetEntry ("tree", TargetFlags.App, 1) };

        treeview1.EnableModelDragDest (ten, Gdk.DragAction.Move);
        treeview1.EnableModelDragSource (Gdk.ModifierType.Button1Mask, ten, Gdk.DragAction.Move);
        ShowAll ();

        treeview1.DragDataGet += (o, args) => {
            TreeModel model;
            ((TreeSelection)treeview1.Selection).GetSelected (out model, out SourceIter);
                args.SelectionData.Set (args.SelectionData.Target, 8,   Encoding.UTF8.GetBytes (model.GetValue (SourceIter, 0).ToString ()));
            };

        treeview1.DragDataReceived += Tree_DragDataReceived;

        treeview1.ButtonPressEvent += Tree_ButtonPressEvent;
    }

	[GLib.ConnectBefore] 
	void Tree_ButtonPressEvent (object o, ButtonPressEventArgs args)
	{ 
		if ((int)args.Event.Button == 3) {
			
            TreeViewContextMenu.ShowAll ();
            TreeViewContextMenu.Popup ();
		}
		Console.WriteLine ("Tree_ButtonPressEvent");
	}

	int depth = -1;

	//Deep Copy SubTree
	void CopySubTree (TreeIter SourceIter, TreeIter DestIter)
	{
		try {
			if (SourceIter.Equals (DestIter)) {
				Console.WriteLine ("不能拖放到自己下");
				return;
			}

			TreeIter ChildrenIter;
			if (SourceIter.Equals (TreeIter.Zero))
				treestore.IterChildren (out ChildrenIter);
			else
				treestore.IterChildren (out ChildrenIter, SourceIter);

			do
            {
				TreeIter NewDestIter = treestore.AppendValues (DestIter, new object[] {
					treestore.GetValue (ChildrenIter, 0),
					treestore.GetValue (ChildrenIter, 1)
				});
				if (treestore.IterHasChild (ChildrenIter)) {
					CopySubTree (ChildrenIter, NewDestIter);
				}
			} while(treestore.IterNext (ref ChildrenIter));
			depth--;
		} catch (Exception ee) {
			Console.WriteLine (ee.Message + "\n" + ee.StackTrace);
		}
	}

	//Show all Descendant
	void TraceTree (TreeIter Iter)
	{
		TreeIter ChildrenIter;
		if (Iter.Equals (TreeIter.Zero))
			treestore.IterChildren (out ChildrenIter);
		else
			treestore.IterChildren (out ChildrenIter, Iter);

		do 
        {
			Console.WriteLine (String.Empty.PadRight (depth * 8, ' ') + treestore.GetValue (ChildrenIter, 0));
			if (treestore.IterHasChild (ChildrenIter)) {
				TraceTree (ChildrenIter);
			}
		} while(treestore.IterNext (ref ChildrenIter));
		depth--;
	}

	[GLib.ConnectBefore] 
	void Tree_DragDataReceived (object o, DragDataReceivedArgs args)
	{
		TreeView treeView = (TreeView)o;
		TreeIter DestIter, TmpNewNodeIter, DestIterParent, SourceIterParent;
		TreePath path;
		TreeViewDropPosition pos;

		//获取目标行path, pos
		if (treeView.GetDestRowAtPos (args.X, args.Y, out path, out pos)) {
			treeView.Model.GetIter (out DestIter, path);

			treestore.IterParent (out DestIterParent, DestIter);
			treestore.IterParent (out SourceIterParent, SourceIter);

			if (DestIter.Equals (SourceIter)) {
				Console.WriteLine ("Can not move to self");
				return;
			}
			switch (pos) {
			case TreeViewDropPosition.Before:
				treeView.SetDragDestRow (path, TreeViewDropPosition.Before);

				//如果是同层节点，则MoveBefore
				if (DestIterParent.Equals (SourceIterParent) && treestore.GetPath (DestIter).Depth == treestore.GetPath (SourceIter).Depth) {
					Console.WriteLine ("同层节点，Before  same level");
					treestore.MoveBefore (SourceIter, DestIter);
				}
				//如果非异层节点
				else {
					TreeIter ParentIter;
					//获取目标节点的父节点
					treestore.IterParent (out ParentIter, DestIter);
					//如果目标节点有父节点，则在父节点添加一个新节点
					if (treestore.IterParent (out ParentIter, DestIter)) {
						//添加新节点
						TmpNewNodeIter = treestore.AppendValues (ParentIter, new object[] {
							treestore.GetValue (SourceIter, 0),
							treestore.GetValue (SourceIter, 1)
						});
					}
					//如果没父节点，则在treenode做为父节点
					else {
						//添加新节点
						TmpNewNodeIter = treestore.AppendValues (new object[] {
							treestore.GetValue (SourceIter, 0),
							treestore.GetValue (SourceIter, 1)
						});
					}

					//移动新节点到目标节点前
					treestore.MoveBefore (TmpNewNodeIter, DestIter);

					//复制SourceIter节点的子节点到TmpNewNodeIter新节点下
					if (treestore.IterHasChild (SourceIter)) {	
						CopySubTree (SourceIter, TmpNewNodeIter);
					}

					//删除源SourceIter节点
					treestore.Remove (ref SourceIter);
				}
				break;

			case TreeViewDropPosition.IntoOrBefore:
				treeView.SetDragDestRow (path, TreeViewDropPosition.IntoOrBefore);
				Console.WriteLine ("IntoOrBefore: ");
				//在DestIter下添加新节点
				TmpNewNodeIter = treestore.AppendValues (DestIter, new object[] {
					treestore.GetValue (SourceIter, 0),
					treestore.GetValue (SourceIter, 1)
				});
				//如果源节点有子节点，则复制子节点
				if (treestore.IterHasChild (SourceIter)) {	
					CopySubTree (SourceIter, TmpNewNodeIter);
				}
				//删除源节点
				treestore.Remove (ref SourceIter);
				break;

			case TreeViewDropPosition.After:
				treeView.SetDragDestRow (path, TreeViewDropPosition.After);
				//同层
				if (DestIterParent.Equals (SourceIterParent) && treestore.GetPath (DestIter).Depth == treestore.GetPath (SourceIter).Depth) {
					Console.WriteLine ("After  same level");
					//直接MoveAfter
					treestore.MoveAfter (SourceIter, DestIter);
				} 
				//异层节点
				else 
                {
					Console.WriteLine ("After Not same level");
					TreeIter ParentIter;

					//如果有父节点
					if (treestore.IterParent (out ParentIter, DestIter)) {
						TmpNewNodeIter = treestore.AppendValues (ParentIter, new object[] {
							treestore.GetValue (SourceIter, 0),
							treestore.GetValue (SourceIter, 1)
						});
					}
                    else 
                    {			
						TmpNewNodeIter = treestore.AppendValues (new object[] {
							treestore.GetValue (SourceIter, 0),
							treestore.GetValue (SourceIter, 1)
						});
					}
					//添加Move新节点到DestIter后
					treestore.MoveAfter (TmpNewNodeIter, DestIter);

					//如果源节点有子节点，则复制子节点
					if (treestore.IterHasChild (SourceIter)) {	
						CopySubTree (SourceIter, TmpNewNodeIter);
					}

					//删除源节点
					treestore.Remove (ref SourceIter);
				}
				break;

			case TreeViewDropPosition.IntoOrAfter:
				Console.WriteLine ("IntoOrAfter");
				treeView.SetDragDestRow (path, TreeViewDropPosition.IntoOrAfter);
				TmpNewNodeIter = treestore.AppendValues (DestIter, new object[] {
					treestore.GetValue (SourceIter, 0),
					treestore.GetValue (SourceIter, 1)
				});
				if (treestore.IterHasChild (SourceIter)) {	
					CopySubTree (SourceIter, TmpNewNodeIter);
				}
				treestore.Remove (ref SourceIter);

				break;
			}
		}
		Gtk.Drag.Finish (args.Context, false, false, args.Time);
	}

    void CreateTreeViewContextMenu()
    {
        TreeViewContextMenu = new Gtk.Menu ();
        //Add Prev
        Gtk.MenuItem item = new MenuItem ("Add Prev");
        item.Name = "MenuItem_AddPrev";
        item.Activated +=   MenuItem_Activated;
        TreeViewContextMenu.Add (item);  

        //Add Next
        item = new MenuItem ("Add Next");
        item.Name = "MenuItem_AddNext";
        item.Activated +=   MenuItem_Activated;
        TreeViewContextMenu.Add (item);

        //Add Sub
        item = new MenuItem ("Add Sub");
        item.Name = "MenuItem_AddSub";
        item.Activated +=   MenuItem_Activated;
        TreeViewContextMenu.Add (item);

        //Delete 
        item = new MenuItem ("Delete This Item");
        item.Name = "MenuItem_Delete";
        item.Activated +=   MenuItem_Activated;
        TreeViewContextMenu.Add (item);         
    }

    void MenuItem_Activated (object sender, EventArgs e)
{        
        MenuItem mitem = (MenuItem)sender;
        TreeIter SelectedIter, NewNodeIter;

        if (mitem.Name.Equals ("MenuItem_AddPrev")) 
        {
            treeview1.Selection.GetSelected (out SelectedIter);
            NewNodeIter = treestore.InsertNodeBefore (SelectedIter);
            treestore.SetValues (NewNodeIter, new object[]{ "new item", "new iterm index" });
        }
        else if (mitem.Name.Equals ("MenuItem_AddNext")) 
        {
            treeview1.Selection.GetSelected (out SelectedIter);
            NewNodeIter = treestore.InsertNodeAfter (SelectedIter);
            treestore.SetValues (NewNodeIter, new object[]{ "new item", "new iterm index" });
        }
        else if (mitem.Name.Equals ("MenuItem_AddSub")) 
        {
            if (treeview1.Selection.GetSelected (out SelectedIter)) 
            {
                treestore.AppendValues (SelectedIter, new object[]{ "new item", "new iterm index" });
            } 
            else
            {
                treestore.AppendValues (new object[]{ "new item", "new iterm index" });
            }
        }
        else if (mitem.Name.Equals ("MenuItem_Delete")) 
        {
            treeview1.Selection.GetSelected (out SelectedIter);
            treestore.Remove (ref SelectedIter);
        }
}

	void Cell_Edited (object o, EditedArgs args)
	{
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}