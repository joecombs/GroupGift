using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Xamarin.Forms;
using GroupGift.Droid;

[assembly: ExportRenderer(typeof(Label), typeof(FontAwesomeLabelRenderer))]
[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(FontAwesomeButtonRenderer))]

namespace GroupGift.Droid
{
    public class FontAwesomeLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            FontAwesomeUtil.CheckAndSetTypeFace(Control);
        }
    }

    public class FontAwesomeButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            FontAwesomeUtil.CheckAndSetTypeFace(Control);
        }
    }

    internal static class FontAwesomeUtil
    {
        public static void CheckAndSetTypeFace(TextView view)
        {
            if (view.Text.Length == 0) return;
            var text = view.Text;
            if (text.Length > 1 || text[0] < 0xf000)
            {
                return;
            }

            var font = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "fontawesome.ttf");
            view.Typeface = font;
        }
    }
}