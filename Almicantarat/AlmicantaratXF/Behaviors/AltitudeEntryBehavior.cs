using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using Xamarin.Forms;
namespace AlmicantaratXF.Behaviors
{
    public class AltitudeEntryBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty BCursorPositionProperty =
            BindableProperty.CreateAttached("BCursorPosition", typeof(int), typeof(AltitudeEntryBehavior), 0,BindingMode.TwoWay);
        public int BCursorPosition
        {
            get { return (int)GetValue(BCursorPositionProperty); }
            set { SetValue(BCursorPositionProperty, value); }
        }
        public static readonly BindableProperty BSLProperty =
            BindableProperty.CreateAttached("BSL", typeof(int), typeof(AltitudeEntryBehavior), 0, BindingMode.TwoWay);
        public int BSL
        {
            get { return (int)GetValue(BSLProperty); }
            set { SetValue(BSLProperty, value); }
        }
        private string mask = "00°00.0'";
        private static string oldTextValue="";
        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            Entry entry = sender as Entry;
            if ((args.NewTextValue != null) && (args.OldTextValue != null))
            {
                if (args.NewTextValue != oldTextValue)
                {
                    int charactersAdded = args.NewTextValue.Length - args.OldTextValue.Length;
                    string text = "";
                    text = args.NewTextValue;
                    int cp = Convert.ToInt32(BCursorPosition);
                    int bsl = BSL;
                    // 1. Adding the MaxLength
                    if (text.Length == mask.Length)
                        entry.MaxLength = mask.Length + 1;
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS: //TBD
                            break;
                        case Device.Android:
                            cp += charactersAdded;
                            break;
                        case Device.UWP:
                            break;
                        default:
                            break;
                    }
                    // Evaluating if some charcters are missing
                    if (args.NewTextValue.Length < mask.Length)
                    {
                        string newTextValue = "";
                        if (args.NewTextValue != null) newTextValue = args.NewTextValue;
                        //remplacer le texte manquant par le masque
                        text = text.Insert(cp, mask.Substring(cp, mask.Length - newTextValue.Length));
                    }
                    // Evaluating if the user add more than one caracter
                    else if ((args.OldTextValue != null) && (args.NewTextValue.Length > args.OldTextValue.Length + 1))
                    {
                        if (args.OldTextValue.Length == mask.Length) text = args.OldTextValue;
                    }
                    //si ajout d'un seul caractère
                    else if ((args.OldTextValue != null) && (args.NewTextValue.Length == args.OldTextValue.Length + 1))
                    {
                        string newChar = "";

                        if (cp >= 1)
                        {
                            if (cp > mask.Length - 1)//dernier caractère qui peut être modifié par l'utilisateur
                            {
                                text = text.Remove(cp - 1, 1);
                            }
                            else
                            {
                                newChar = text.Substring(cp - 1, 1);
                                if (Regex.IsMatch(newChar, "[0-9]"))
                                {
                                    if (mask.Substring(cp - 1, 1) == "0")
                                    {
                                        text = text.Remove(cp, 1);
                                    }
                                    else if (mask.Substring(cp, 1) == "0")
                                    {
                                        text = text.Substring(0, cp - 1) + mask.Substring(cp - 1, 1) + newChar + text.Substring(cp + 2);
                                    }
                                }
                                else
                                {
                                    text = text.Remove(cp - 1, 1);
                                }
                            }
                        }
                    }
                    entry.Text = text;
                    oldTextValue = text;
                    BCursorPosition = cp;
                }
            }
        }
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }
        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }
    }
}
