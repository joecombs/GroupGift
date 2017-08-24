using Android.Content;
using Android.Database;
using Android.Provider;
using System;

namespace GroupGift.Droid
{
    public class ContactService
    {
        public static ICursor GetContactCursor(ContentResolver contactHelper)
        {
            String[] projection = { "_id", "display_name", ContactsContract.CommonDataKinds.Phone.Number };
            ICursor cur = null;
            try
            {
                cur = contactHelper.Query(ContactsContract.CommonDataKinds.Phone.ContentUri, projection, null, null, "display_name ASC");
                cur.MoveToFirst();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

           
        private static long GetContactID(ContentResolver contactHelper, string number)
        {
            Android.Net.Uri contacturi = Android.Net.Uri.WithAppendedPath(ContactsContract.PhoneLookup.ContentFilterUri, Android.Net.Uri.Encode(number));
            String[] projection = { "_id" };
            ICursor cur = null;
            try
            {
                cur = contactHelper.Query(contacturi, projection, null, null, null);
                if (cur.MoveToFirst())
                {
                    int personId = cur.GetColumnIndex("_id");
                    return cur.GetLong(personId);
                }
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;

        }

    }
}