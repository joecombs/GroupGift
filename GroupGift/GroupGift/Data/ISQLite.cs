using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupGift.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
