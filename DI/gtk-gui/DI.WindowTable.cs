
// This file has been generated by the GUI designer. Do not modify.
namespace DI
{
	public partial class WindowTable
	{
		private global::Gtk.Fixed fixed1;

		private global::Gtk.Label label1;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.TreeView treeview1;

		private global::Gtk.ScrolledWindow GtkScrolledWindow1;

		private global::Gtk.TreeView treeview2;

		private global::Gtk.Button button1;

		private global::Gtk.Button button2;

		private global::Gtk.Button button3;

		private global::Gtk.Button button4;

		private global::Gtk.Button button6;

		private global::Gtk.Button button5;

		private global::Gtk.Button button7;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget DI.WindowTable
			this.Name = "DI.WindowTable";
			this.Title = global::Mono.Unix.Catalog.GetString("WindowTable");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child DI.WindowTable.Gtk.Container+ContainerChild
			this.fixed1 = new global::Gtk.Fixed();
			this.fixed1.Name = "fixed1";
			this.fixed1.HasWindow = false;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Бази даних");
			this.fixed1.Add(this.label1);
			global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.label1]));
			w1.X = 11;
			w1.Y = 8;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeview1 = new global::Gtk.TreeView();
			this.treeview1.WidthRequest = 175;
			this.treeview1.HeightRequest = 250;
			this.treeview1.CanFocus = true;
			this.treeview1.Name = "treeview1";
			this.treeview1.Reorderable = true;
			this.treeview1.RulesHint = true;
			this.GtkScrolledWindow.Add(this.treeview1);
			this.fixed1.Add(this.GtkScrolledWindow);
			global::Gtk.Fixed.FixedChild w3 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.GtkScrolledWindow]));
			w3.X = 24;
			w3.Y = 35;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.treeview2 = new global::Gtk.TreeView();
			this.treeview2.WidthRequest = 800;
			this.treeview2.HeightRequest = 400;
			this.treeview2.CanFocus = true;
			this.treeview2.Name = "treeview2";
			this.treeview2.Reorderable = true;
			this.treeview2.RulesHint = true;
			this.GtkScrolledWindow1.Add(this.treeview2);
			this.fixed1.Add(this.GtkScrolledWindow1);
			global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.GtkScrolledWindow1]));
			w5.X = 234;
			w5.Y = 34;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.button1 = new global::Gtk.Button();
			this.button1.WidthRequest = 190;
			this.button1.HeightRequest = 40;
			this.button1.CanFocus = true;
			this.button1.Name = "button1";
			this.button1.UseUnderline = true;
			this.button1.Label = global::Mono.Unix.Catalog.GetString("Створити БД");
			this.fixed1.Add(this.button1);
			global::Gtk.Fixed.FixedChild w6 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.button1]));
			w6.X = 25;
			w6.Y = 317;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.button2 = new global::Gtk.Button();
			this.button2.WidthRequest = 190;
			this.button2.HeightRequest = 40;
			this.button2.CanFocus = true;
			this.button2.Name = "button2";
			this.button2.UseUnderline = true;
			this.button2.Label = global::Mono.Unix.Catalog.GetString("Додати запис");
			this.fixed1.Add(this.button2);
			global::Gtk.Fixed.FixedChild w7 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.button2]));
			w7.X = 25;
			w7.Y = 417;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.button3 = new global::Gtk.Button();
			this.button3.WidthRequest = 190;
			this.button3.HeightRequest = 40;
			this.button3.CanFocus = true;
			this.button3.Name = "button3";
			this.button3.UseUnderline = true;
			this.button3.Label = global::Mono.Unix.Catalog.GetString("Видалити запис");
			this.fixed1.Add(this.button3);
			global::Gtk.Fixed.FixedChild w8 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.button3]));
			w8.X = 25;
			w8.Y = 467;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.button4 = new global::Gtk.Button();
			this.button4.WidthRequest = 190;
			this.button4.HeightRequest = 40;
			this.button4.CanFocus = true;
			this.button4.Name = "button4";
			this.button4.UseUnderline = true;
			this.button4.Label = global::Mono.Unix.Catalog.GetString("Додати юзера");
			this.fixed1.Add(this.button4);
			global::Gtk.Fixed.FixedChild w9 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.button4]));
			w9.X = 25;
			w9.Y = 367;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.button6 = new global::Gtk.Button();
			this.button6.WidthRequest = 120;
			this.button6.CanFocus = true;
			this.button6.Name = "button6";
			this.button6.UseUnderline = true;
			this.button6.Label = global::Mono.Unix.Catalog.GetString("Save as Word");
			this.fixed1.Add(this.button6);
			global::Gtk.Fixed.FixedChild w10 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.button6]));
			w10.X = 915;
			w10.Y = 466;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.button5 = new global::Gtk.Button();
			this.button5.WidthRequest = 120;
			this.button5.CanFocus = true;
			this.button5.Name = "button5";
			this.button5.UseUnderline = true;
			this.button5.Label = global::Mono.Unix.Catalog.GetString("Save as Excel");
			this.fixed1.Add(this.button5);
			global::Gtk.Fixed.FixedChild w11 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.button5]));
			w11.X = 915;
			w11.Y = 506;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.button7 = new global::Gtk.Button();
			this.button7.WidthRequest = 120;
			this.button7.CanFocus = true;
			this.button7.Name = "button7";
			this.button7.UseUnderline = true;
			this.button7.Label = global::Mono.Unix.Catalog.GetString("Save as PDF");
			this.fixed1.Add(this.button7);
			global::Gtk.Fixed.FixedChild w12 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.button7]));
			w12.X = 915;
			w12.Y = 546;
			this.Add(this.fixed1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 1103;
			this.DefaultHeight = 607;
			this.Show();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
			this.treeview1.RowActivated += new global::Gtk.RowActivatedHandler(this.OnTreeview1RowActivated);
			this.treeview2.RowActivated += new global::Gtk.RowActivatedHandler(this.OnTreeview2RowActivated);
			this.button1.Pressed += new global::System.EventHandler(this.OnButton1Pressed);
			this.button2.Pressed += new global::System.EventHandler(this.OnButton2Pressed);
			this.button3.Pressed += new global::System.EventHandler(this.OnButton3Pressed);
			this.button4.Pressed += new global::System.EventHandler(this.OnButton4Pressed);
			this.button6.Pressed += new global::System.EventHandler(this.OnButton6Pressed);
			this.button5.Pressed += new global::System.EventHandler(this.OnButton5Pressed);
			this.button7.Pressed += new global::System.EventHandler(this.OnButton7Pressed);
		}
	}
}
