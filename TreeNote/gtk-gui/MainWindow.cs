
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	
	private global::Gtk.Action directoryAction;
	
	private global::Gtk.Action saveAction;
	
	private global::Gtk.Action floppyAction;
	
	private global::Gtk.Action selectColorAction1;
	
	private global::Gtk.Action selectColorAction;
	
	private global::Gtk.Action copyAction;
	
	private global::Gtk.Action cutAction;
	
	private global::Gtk.Action pasteAction;
	
	private global::Gtk.Action deleteAction;
	
	private global::Gtk.Action undoAction;
	
	private global::Gtk.Action redoAction;
	
	private global::Gtk.Action boldAction;
	
	private global::Gtk.Action italicAction;
	
	private global::Gtk.Action strikethroughAction;
	
	private global::Gtk.Action underlineAction;
	
	private global::Gtk.Action indentAction;
	
	private global::Gtk.Action TreeNoteAction;
	
	private global::Gtk.Action picsAction;
	
	private global::Gtk.Action newAction;
	
	private global::Gtk.Action Action;
	
	private global::Gtk.Action Action1;
	
	private global::Gtk.Action Action2;
	
	private global::Gtk.Action Action3;
	
	private global::Gtk.Action Action4;
	
	private global::Gtk.Action Action5;
	
	private global::Gtk.Action Action6;
	
	private global::Gtk.Action Action7;
	
	private global::Gtk.Action AAAAction;
	
	private global::Gtk.Action BBBBAction;
	
	private global::Gtk.Action Action8;
	
	private global::Gtk.Action Action9;
	
	private global::Gtk.VBox vbox1;
	
	private global::Gtk.MenuBar menubar1;
	
	private global::Gtk.Toolbar toolbar1;
	
	private global::Gtk.HPaned hpaned1;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	
	private global::Gtk.TreeView treeview1;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow1;
	
	private global::Gtk.TextView textview1;
	
	private global::Gtk.Statusbar statusbar1;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.directoryAction = new global::Gtk.Action ("directoryAction", null, null, "gtk-directory");
		w1.Add (this.directoryAction, null);
		this.saveAction = new global::Gtk.Action ("saveAction", null, null, "gtk-save");
		w1.Add (this.saveAction, null);
		this.floppyAction = new global::Gtk.Action ("floppyAction", null, null, "gtk-floppy");
		w1.Add (this.floppyAction, null);
		this.selectColorAction1 = new global::Gtk.Action ("selectColorAction1", null, null, "gtk-select-color");
		w1.Add (this.selectColorAction1, null);
		this.selectColorAction = new global::Gtk.Action ("selectColorAction", null, null, "gtk-select-color");
		w1.Add (this.selectColorAction, null);
		this.copyAction = new global::Gtk.Action ("copyAction", null, null, "gtk-copy");
		w1.Add (this.copyAction, null);
		this.cutAction = new global::Gtk.Action ("cutAction", null, null, "gtk-cut");
		w1.Add (this.cutAction, null);
		this.pasteAction = new global::Gtk.Action ("pasteAction", null, null, "gtk-paste");
		w1.Add (this.pasteAction, null);
		this.deleteAction = new global::Gtk.Action ("deleteAction", null, null, "gtk-delete");
		w1.Add (this.deleteAction, null);
		this.undoAction = new global::Gtk.Action ("undoAction", null, null, "gtk-undo");
		w1.Add (this.undoAction, null);
		this.redoAction = new global::Gtk.Action ("redoAction", null, null, "gtk-redo");
		w1.Add (this.redoAction, null);
		this.boldAction = new global::Gtk.Action ("boldAction", null, null, "gtk-bold");
		w1.Add (this.boldAction, null);
		this.italicAction = new global::Gtk.Action ("italicAction", null, null, "gtk-italic");
		w1.Add (this.italicAction, null);
		this.strikethroughAction = new global::Gtk.Action ("strikethroughAction", null, null, "gtk-strikethrough");
		w1.Add (this.strikethroughAction, null);
		this.underlineAction = new global::Gtk.Action ("underlineAction", null, null, "gtk-underline");
		w1.Add (this.underlineAction, null);
		this.indentAction = new global::Gtk.Action ("indentAction", null, null, "gtk-indent");
		w1.Add (this.indentAction, null);
		this.TreeNoteAction = new global::Gtk.Action ("TreeNoteAction", null, null, "TreeNote");
		w1.Add (this.TreeNoteAction, null);
		this.picsAction = new global::Gtk.Action ("picsAction", null, null, "pics");
		w1.Add (this.picsAction, null);
		this.newAction = new global::Gtk.Action ("newAction", null, null, "gtk-new");
		w1.Add (this.newAction, null);
		this.Action = new global::Gtk.Action ("Action", global::Mono.Unix.Catalog.GetString ("文件"), null, null);
		this.Action.ShortLabel = global::Mono.Unix.Catalog.GetString ("文件");
		w1.Add (this.Action, null);
		this.Action1 = new global::Gtk.Action ("Action1", global::Mono.Unix.Catalog.GetString ("新建"), null, null);
		this.Action1.ShortLabel = global::Mono.Unix.Catalog.GetString ("新建");
		w1.Add (this.Action1, null);
		this.Action2 = new global::Gtk.Action ("Action2", global::Mono.Unix.Catalog.GetString ("打开"), null, null);
		this.Action2.ShortLabel = global::Mono.Unix.Catalog.GetString ("打开");
		w1.Add (this.Action2, null);
		this.Action3 = new global::Gtk.Action ("Action3", global::Mono.Unix.Catalog.GetString ("保存"), null, null);
		this.Action3.ShortLabel = global::Mono.Unix.Catalog.GetString ("保存");
		w1.Add (this.Action3, null);
		this.Action4 = new global::Gtk.Action ("Action4", global::Mono.Unix.Catalog.GetString ("另存为"), null, null);
		this.Action4.ShortLabel = global::Mono.Unix.Catalog.GetString ("另存为");
		w1.Add (this.Action4, null);
		this.Action5 = new global::Gtk.Action ("Action5", global::Mono.Unix.Catalog.GetString ("关闭"), null, null);
		this.Action5.ShortLabel = global::Mono.Unix.Catalog.GetString ("关闭");
		w1.Add (this.Action5, null);
		this.Action6 = new global::Gtk.Action ("Action6", global::Mono.Unix.Catalog.GetString ("---"), null, null);
		this.Action6.ShortLabel = global::Mono.Unix.Catalog.GetString ("---");
		w1.Add (this.Action6, null);
		this.Action7 = new global::Gtk.Action ("Action7", global::Mono.Unix.Catalog.GetString ("历史文件"), null, null);
		this.Action7.ShortLabel = global::Mono.Unix.Catalog.GetString ("历史文件");
		w1.Add (this.Action7, null);
		this.AAAAction = new global::Gtk.Action ("AAAAction", global::Mono.Unix.Catalog.GetString ("AAA"), null, null);
		this.AAAAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("AAA");
		w1.Add (this.AAAAction, null);
		this.BBBBAction = new global::Gtk.Action ("BBBBAction", global::Mono.Unix.Catalog.GetString ("BBBB"), null, null);
		this.BBBBAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("BBBB");
		w1.Add (this.BBBBAction, null);
		this.Action8 = new global::Gtk.Action ("Action8", global::Mono.Unix.Catalog.GetString ("编辑"), null, null);
		this.Action8.ShortLabel = global::Mono.Unix.Catalog.GetString ("编辑");
		w1.Add (this.Action8, null);
		this.Action9 = new global::Gtk.Action ("Action9", global::Mono.Unix.Catalog.GetString ("配置"), null, null);
		this.Action9.ShortLabel = global::Mono.Unix.Catalog.GetString ("配置");
		w1.Add (this.Action9, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.CanFocus = true;
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><menubar name='menubar1'><menu name='Action' action='Action'><menuitem name='Action1' action='Action1'/><menuitem name='Action2' action='Action2'/><menuitem name='Action3' action='Action3'/><menuitem name='Action4' action='Action4'/><menuitem name='Action5' action='Action5'/><menuitem name='Action6' action='Action6'/><menu name='Action7' action='Action7'><menuitem name='AAAAction' action='AAAAction'/><menuitem name='BBBBAction' action='BBBBAction'/></menu></menu><menu name='Action8' action='Action8'/><menu name='Action9' action='Action9'/></menubar></ui>");
		this.menubar1 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar1")));
		this.menubar1.Name = "menubar1";
		this.vbox1.Add (this.menubar1);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.menubar1]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><toolbar name='toolbar1'><toolitem name='newAction' action='newAction'/><toolitem name='directoryAction' action='directoryAction'/><toolitem name='saveAction' action='saveAction'/><toolitem name='floppyAction' action='floppyAction'/><toolitem name='selectColorAction1' action='selectColorAction1'/><toolitem name='selectColorAction' action='selectColorAction'/><toolitem name='copyAction' action='copyAction'/><toolitem name='cutAction' action='cutAction'/><toolitem name='pasteAction' action='pasteAction'/><toolitem name='deleteAction' action='deleteAction'/><toolitem name='undoAction' action='undoAction'/><toolitem name='redoAction' action='redoAction'/><toolitem name='boldAction' action='boldAction'/><toolitem name='italicAction' action='italicAction'/><toolitem name='strikethroughAction' action='strikethroughAction'/><toolitem name='underlineAction' action='underlineAction'/><toolitem name='indentAction' action='indentAction'/><toolitem name='TreeNoteAction' action='TreeNoteAction'/></toolbar></ui>");
		this.toolbar1 = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/toolbar1")));
		this.toolbar1.Name = "toolbar1";
		this.toolbar1.ShowArrow = false;
		this.vbox1.Add (this.toolbar1);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.toolbar1]));
		w3.Position = 1;
		w3.Expand = false;
		w3.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.hpaned1 = new global::Gtk.HPaned ();
		this.hpaned1.WidthRequest = 300;
		this.hpaned1.CanDefault = true;
		this.hpaned1.CanFocus = true;
		this.hpaned1.Name = "hpaned1";
		this.hpaned1.Position = 183;
		this.hpaned1.BorderWidth = ((uint)(2));
		// Container child hpaned1.Gtk.Paned+PanedChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.WidthRequest = 180;
		this.GtkScrolledWindow.Events = ((global::Gdk.EventMask)(32));
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.treeview1 = new global::Gtk.TreeView ();
		this.treeview1.WidthRequest = 100;
		this.treeview1.CanFocus = true;
		this.treeview1.Name = "treeview1";
		this.treeview1.HeadersVisible = false;
		this.GtkScrolledWindow.Add (this.treeview1);
		this.hpaned1.Add (this.GtkScrolledWindow);
		global::Gtk.Paned.PanedChild w5 = ((global::Gtk.Paned.PanedChild)(this.hpaned1 [this.GtkScrolledWindow]));
		w5.Resize = false;
		// Container child hpaned1.Gtk.Paned+PanedChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow1.WidthRequest = 400;
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.textview1 = new global::Gtk.TextView ();
		this.textview1.WidthRequest = 200;
		this.textview1.CanFocus = true;
		this.textview1.Name = "textview1";
		this.GtkScrolledWindow1.Add (this.textview1);
		this.hpaned1.Add (this.GtkScrolledWindow1);
		this.vbox1.Add (this.hpaned1);
		global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hpaned1]));
		w8.Position = 2;
		w8.Padding = ((uint)(10));
		// Container child vbox1.Gtk.Box+BoxChild
		this.statusbar1 = new global::Gtk.Statusbar ();
		this.statusbar1.Name = "statusbar1";
		this.statusbar1.Spacing = 6;
		this.vbox1.Add (this.statusbar1);
		global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.statusbar1]));
		w9.Position = 3;
		w9.Expand = false;
		w9.Fill = false;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 806;
		this.DefaultHeight = 300;
		this.hpaned1.HasDefault = true;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
	}
}
