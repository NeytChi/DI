
// This file has been generated by the GUI designer. Do not modify.
namespace DI
{
	public partial class WCreateUser
	{
		private global::Gtk.Fixed fixed1;

		private global::Gtk.Button button1;

		private global::Gtk.Entry entry1;

		private global::Gtk.Entry entry2;

		private global::Gtk.Label label2;

		private global::Gtk.Label label1;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget DI.WCreateUser
			this.Name = "DI.WCreateUser";
			this.Title = global::Mono.Unix.Catalog.GetString("WCreateUser");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child DI.WCreateUser.Gtk.Container+ContainerChild
			this.fixed1 = new global::Gtk.Fixed();
			this.fixed1.Name = "fixed1";
			this.fixed1.HasWindow = false;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.button1 = new global::Gtk.Button();
			this.button1.CanFocus = true;
			this.button1.Name = "button1";
			this.button1.UseUnderline = true;
			this.button1.Label = global::Mono.Unix.Catalog.GetString("Додати");
			this.fixed1.Add(this.button1);
			global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.button1]));
			w1.X = 275;
			w1.Y = 137;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.entry1 = new global::Gtk.Entry();
			this.entry1.WidthRequest = 200;
			this.entry1.HeightRequest = 40;
			this.entry1.CanFocus = true;
			this.entry1.Name = "entry1";
			this.entry1.IsEditable = true;
			this.entry1.InvisibleChar = '•';
			this.fixed1.Add(this.entry1);
			global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.entry1]));
			w2.X = 100;
			w2.Y = 10;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.entry2 = new global::Gtk.Entry();
			this.entry2.WidthRequest = 200;
			this.entry2.HeightRequest = 40;
			this.entry2.CanFocus = true;
			this.entry2.Name = "entry2";
			this.entry2.IsEditable = true;
			this.entry2.Visibility = false;
			this.entry2.InvisibleChar = '•';
			this.fixed1.Add(this.entry2);
			global::Gtk.Fixed.FixedChild w3 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.entry2]));
			w3.X = 100;
			w3.Y = 50;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Пароль");
			this.fixed1.Add(this.label2);
			global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.label2]));
			w4.X = 28;
			w4.Y = 58;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Им\'я");
			this.fixed1.Add(this.label1);
			global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.label1]));
			w5.X = 31;
			w5.Y = 25;
			this.Add(this.fixed1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 364;
			this.DefaultHeight = 179;
			this.Show();
			this.button1.Pressed += new global::System.EventHandler(this.OnButton1Pressed);
		}
	}
}
