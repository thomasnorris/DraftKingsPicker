using Microsoft.VisualBasic.FileIO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftKingsUpdater
{
    class Program
    {
        private static MySQLService _db;

        static void Main(string[] args)
        {
            _db = new MySQLService();

            try
            {
                _db.OpenConnection();

                // always update draft kings first
                UpdateDraftKings("SP_InsertOrUpdateDraftKingsPayer", "DraftKingsData.csv");

                // update everything else
                UpdatePassingStats("SP_InsertOrUpdatePassingStats", "NFLStats-Passing.csv");
                UpdateRushingStats("SP_InsertOrUpdateRushingStats", "NFLStats-Rushing.csv");
                UpdateReceivingStats("SP_InsertOrUpdateReceivingStats", "NFLStats-Receiving.csv");
                UpdateFumbleStats("SP_InsertOrUpdateFumbleStats", "NFLStats-Fumbles.csv");
                UpdateTackleStats("SP_InsertOrUpdateTackleStats", "NFLStats-Tackles.csv");
                UpdateInterceptionStats("SP_InsertOrUpdateInterceptionStats", "NFLStats-Interceptions.csv");
                UpdateFieldGoalStats("SP_InsertOrUpdateFieldGoalStats", "NFLStats-Field-Goals.csv");
                UpdateKickoffStats("SP_InsertOrUpdateKickoffStats", "NFLStats-Kickoffs.csv");
                UpdateKickoffReturnStats("SP_InsertOrUpdateKickoffReturnStats", "NFLStats-Kickoff-Returns.csv");
                UpdatePuntingStats("SP_InsertOrUpdatePuntingStats", "NFLStats-Punting.csv");
                UpdatePuntReturnStats("SP_InsertOrUpdatePuntReturnStats", "NFLStats-Punt-Returns.csv");

                _db.CloseConnection();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static void UpdatePuntReturnStats(string proc, string csv)
        {
            Console.WriteLine("Updating Punt Return Stats...");
            using (TextFieldParser parser = new TextFieldParser(csv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields();
                    var mysqlArgs = new List<MySqlParameter>();

                    mysqlArgs.Add(new MySqlParameter("player_name", row[0]));
                    mysqlArgs.Add(new MySqlParameter("avg", row[1]));
                    mysqlArgs.Add(new MySqlParameter("ret", row[2]));
                    mysqlArgs.Add(new MySqlParameter("yds", row[3]));
                    mysqlArgs.Add(new MySqlParameter("pret_t", row[4]));
                    mysqlArgs.Add(new MySqlParameter("twenty_plus", row[5]));
                    mysqlArgs.Add(new MySqlParameter("fourty_plus", row[6]));
                    mysqlArgs.Add(new MySqlParameter("lng", row[7]));
                    mysqlArgs.Add(new MySqlParameter("fc", row[8]));
                    mysqlArgs.Add(new MySqlParameter("fum", row[9]));

                    _db.ExecuteStoredProc(proc, mysqlArgs);
                }
            }
        }

        private static void UpdatePuntingStats(string proc, string csv)
        {
            Console.WriteLine("Updating Punting Stats...");
            using (TextFieldParser parser = new TextFieldParser(csv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields();
                    var mysqlArgs = new List<MySqlParameter>();

                    mysqlArgs.Add(new MySqlParameter("player_name", row[0]));
                    mysqlArgs.Add(new MySqlParameter("avg", row[1]));
                    mysqlArgs.Add(new MySqlParameter("net_avg", row[2]));
                    mysqlArgs.Add(new MySqlParameter("net_yds", row[3]));
                    mysqlArgs.Add(new MySqlParameter("punts", row[4]));
                    mysqlArgs.Add(new MySqlParameter("lng", row[5]));
                    mysqlArgs.Add(new MySqlParameter("yds", row[6]));
                    mysqlArgs.Add(new MySqlParameter("in_twenty", row[7]));
                    mysqlArgs.Add(new MySqlParameter("oob", row[8]));
                    mysqlArgs.Add(new MySqlParameter("dn", row[9]));
                    mysqlArgs.Add(new MySqlParameter("tb", row[10]));
                    mysqlArgs.Add(new MySqlParameter("fc", row[11]));
                    mysqlArgs.Add(new MySqlParameter("ret", row[12]));
                    mysqlArgs.Add(new MySqlParameter("rety", row[13]));
                    mysqlArgs.Add(new MySqlParameter("td", row[14]));
                    mysqlArgs.Add(new MySqlParameter("p_blk", row[15]));

                    _db.ExecuteStoredProc(proc, mysqlArgs);
                }
            }
        }

        private static void UpdateKickoffReturnStats(string proc, string csv)
        {
            Console.WriteLine("Updating Kickoff Return Stats...");
            using (TextFieldParser parser = new TextFieldParser(csv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields();
                    var mysqlArgs = new List<MySqlParameter>();

                    mysqlArgs.Add(new MySqlParameter("player_name", row[0]));
                    mysqlArgs.Add(new MySqlParameter("avg", row[1]));
                    mysqlArgs.Add(new MySqlParameter("ret", row[2]));
                    mysqlArgs.Add(new MySqlParameter("yds", row[3]));
                    mysqlArgs.Add(new MySqlParameter("kret_td", row[4]));
                    mysqlArgs.Add(new MySqlParameter("twenty_plus", row[5]));
                    mysqlArgs.Add(new MySqlParameter("fourty_plus", row[6]));
                    mysqlArgs.Add(new MySqlParameter("lng", row[7]));
                    mysqlArgs.Add(new MySqlParameter("fc", row[8]));
                    mysqlArgs.Add(new MySqlParameter("fum", row[9]));

                    _db.ExecuteStoredProc(proc, mysqlArgs);
                }
            }
        }

        private static void UpdateKickoffStats(string proc, string csv)
        {
            Console.WriteLine("Updating Kickoff Stats...");
            using (TextFieldParser parser = new TextFieldParser(csv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields();
                    var mysqlArgs = new List<MySqlParameter>();

                    mysqlArgs.Add(new MySqlParameter("player_name", row[0]));
                    mysqlArgs.Add(new MySqlParameter("ko", row[1]));
                    mysqlArgs.Add(new MySqlParameter("ko_yds", row[2]));
                    mysqlArgs.Add(new MySqlParameter("ret_yds", row[3]));
                    mysqlArgs.Add(new MySqlParameter("tb", row[4]));
                    mysqlArgs.Add(new MySqlParameter("tb_perc", row[5]));
                    mysqlArgs.Add(new MySqlParameter("ret", row[6]));
                    mysqlArgs.Add(new MySqlParameter("ret_avg", row[7]));
                    mysqlArgs.Add(new MySqlParameter("osk", row[8]));
                    mysqlArgs.Add(new MySqlParameter("osk_rec", row[9]));
                    mysqlArgs.Add(new MySqlParameter("oob", row[10]));
                    mysqlArgs.Add(new MySqlParameter("td", row[11]));

                    _db.ExecuteStoredProc(proc, mysqlArgs);
                }
            }
        }

        private static void UpdateFieldGoalStats(string proc, string csv)
        {
            Console.WriteLine("Updating Field Goal Stats...");
            using (TextFieldParser parser = new TextFieldParser(csv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields();
                    var mysqlArgs = new List<MySqlParameter>();

                    mysqlArgs.Add(new MySqlParameter("player_name", row[0]));
                    mysqlArgs.Add(new MySqlParameter("fgm", row[1]));
                    mysqlArgs.Add(new MySqlParameter("att", row[2]));
                    mysqlArgs.Add(new MySqlParameter("fg_perc", row[3]));

                    var am = row[4].Split('/').ToList();
                    if (am.Count() != 2)
                    {
                        am.Add("-");
                    }
                    mysqlArgs.Add(new MySqlParameter("one_to_nineteen_a", am[0]));
                    mysqlArgs.Add(new MySqlParameter("one_to_nineteen_m", am[1]));

                    am = row[5].Split('/').ToList();
                    if (am.Count() != 2)
                    {
                        am.Add("-");
                    }
                    mysqlArgs.Add(new MySqlParameter("twenty_to_twenty_nine_a", am[0]));
                    mysqlArgs.Add(new MySqlParameter("twenty_to_twenty_nine_m", am[1]));

                    am = row[6].Split('/').ToList();
                    if (am.Count() != 2)
                    {
                        am.Add("-");
                    }
                    mysqlArgs.Add(new MySqlParameter("thirty_to_thirty_nine_a", am[0]));
                    mysqlArgs.Add(new MySqlParameter("thirty_to_thirty_nine_m", am[1]));

                    am = row[7].Split('/').ToList();
                    if (am.Count() != 2)
                    {
                        am.Add("-");
                    }
                    mysqlArgs.Add(new MySqlParameter("fourty_to_fourty_nine_a", am[0]));
                    mysqlArgs.Add(new MySqlParameter("fourty_to_fourty_nine_m", am[1]));

                    am = row[8].Split('/').ToList();
                    if (am.Count() != 2)
                    {
                        am.Add("-");
                    }
                    mysqlArgs.Add(new MySqlParameter("fifty_plus_a", am[0]));
                    mysqlArgs.Add(new MySqlParameter("fifty_plus_m", am[1]));
                    
                    mysqlArgs.Add(new MySqlParameter("lng", row[9]));
                    mysqlArgs.Add(new MySqlParameter("fg_blk", row[10]));

                    _db.ExecuteStoredProc(proc, mysqlArgs);
                }
            }
        }

        private static void UpdateInterceptionStats(string proc, string csv)
        {
            Console.WriteLine("Updating Interception Stats...");
            using (TextFieldParser parser = new TextFieldParser(csv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields();
                    var mysqlArgs = new List<MySqlParameter>();

                    mysqlArgs.Add(new MySqlParameter("player_name", row[0]));
                    mysqlArgs.Add(new MySqlParameter("_int", row[1]));
                    mysqlArgs.Add(new MySqlParameter("int_td", row[2]));
                    mysqlArgs.Add(new MySqlParameter("int_yds", row[3]));
                    mysqlArgs.Add(new MySqlParameter("lng", row[4]));

                    _db.ExecuteStoredProc(proc, mysqlArgs);
                }
            }
        }

        private static void UpdateTackleStats(string proc, string csv)
        {
            Console.WriteLine("Updating Tackle Stats...");
            using (TextFieldParser parser = new TextFieldParser(csv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields();
                    var mysqlArgs = new List<MySqlParameter>();

                    mysqlArgs.Add(new MySqlParameter("player_name", row[0]));
                    mysqlArgs.Add(new MySqlParameter("comb", row[1]));
                    mysqlArgs.Add(new MySqlParameter("asst", row[2]));
                    mysqlArgs.Add(new MySqlParameter("solo", row[3]));
                    mysqlArgs.Add(new MySqlParameter("sck", row[4]));

                    _db.ExecuteStoredProc(proc, mysqlArgs);
                }
            }
        }

        private static void UpdateFumbleStats(string proc, string csv)
        {
            Console.WriteLine("Updating Fumble Stats...");
            using (TextFieldParser parser = new TextFieldParser(csv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields();
                    var mysqlArgs = new List<MySqlParameter>();

                    mysqlArgs.Add(new MySqlParameter("player_name", row[0]));
                    mysqlArgs.Add(new MySqlParameter("ff", row[1]));
                    mysqlArgs.Add(new MySqlParameter("fr", row[2]));
                    mysqlArgs.Add(new MySqlParameter("fr_td", row[3]));

                    _db.ExecuteStoredProc(proc, mysqlArgs);
                }
            }
        }

        private static void UpdateReceivingStats(string proc, string csv)
        {
            Console.WriteLine("Updating Receiving Stats...");
            using (TextFieldParser parser = new TextFieldParser(csv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields();
                    var mysqlArgs = new List<MySqlParameter>();

                    mysqlArgs.Add(new MySqlParameter("player_name", row[0]));
                    mysqlArgs.Add(new MySqlParameter("rec", row[1]));
                    mysqlArgs.Add(new MySqlParameter("yds", row[2]));
                    mysqlArgs.Add(new MySqlParameter("td", row[3]));
                    mysqlArgs.Add(new MySqlParameter("twenty_plus", row[4]));
                    mysqlArgs.Add(new MySqlParameter("fourty_plus", row[5]));
                    mysqlArgs.Add(new MySqlParameter("lng", row[6]));
                    mysqlArgs.Add(new MySqlParameter("rec_first", row[7]));
                    mysqlArgs.Add(new MySqlParameter("first_perc", row[8]));
                    mysqlArgs.Add(new MySqlParameter("rec_fum", row[9]));
                    mysqlArgs.Add(new MySqlParameter("rec_yac_per_r", row[10]));
                    mysqlArgs.Add(new MySqlParameter("tgts", row[11]));

                    _db.ExecuteStoredProc(proc, mysqlArgs);
                }
            }
        }

        private static void UpdateRushingStats(string proc, string csv)
        {
            Console.WriteLine("Updating Rushing Stats...");
            using (TextFieldParser parser = new TextFieldParser(csv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields();
                    var mysqlArgs = new List<MySqlParameter>();

                    mysqlArgs.Add(new MySqlParameter("player_name", row[0]));
                    mysqlArgs.Add(new MySqlParameter("rush_yds", row[1]));
                    mysqlArgs.Add(new MySqlParameter("att", row[2]));
                    mysqlArgs.Add(new MySqlParameter("td", row[3]));
                    mysqlArgs.Add(new MySqlParameter("twenty_plus", row[4]));
                    mysqlArgs.Add(new MySqlParameter("fourty_plus", row[5]));
                    mysqlArgs.Add(new MySqlParameter("lng", row[6]));
                    mysqlArgs.Add(new MySqlParameter("rush_first", row[7]));
                    mysqlArgs.Add(new MySqlParameter("rush_first_perc", row[8]));
                    mysqlArgs.Add(new MySqlParameter("rush_fum", row[9]));

                    _db.ExecuteStoredProc(proc, mysqlArgs);
                }
            }
        }

        private static void UpdatePassingStats(string proc, string csv)
        {
            Console.WriteLine("Updating Passing Stats...");
            using (TextFieldParser parser = new TextFieldParser(csv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields();
                    var mysqlArgs = new List<MySqlParameter>();

                    mysqlArgs.Add(new MySqlParameter("player_name", row[0]));
                    mysqlArgs.Add(new MySqlParameter("pass_yds", row[1]));
                    mysqlArgs.Add(new MySqlParameter("yds_per_att", row[2]));
                    mysqlArgs.Add(new MySqlParameter("att", row[3]));
                    mysqlArgs.Add(new MySqlParameter("cmp", row[4]));
                    mysqlArgs.Add(new MySqlParameter("cmp_perc", row[5]));
                    mysqlArgs.Add(new MySqlParameter("td", row[6]));
                    mysqlArgs.Add(new MySqlParameter("intercept", row[7]));
                    mysqlArgs.Add(new MySqlParameter("rate", row[8]));
                    mysqlArgs.Add(new MySqlParameter("first", row[9]));
                    mysqlArgs.Add(new MySqlParameter("first_perc", row[10]));
                    mysqlArgs.Add(new MySqlParameter("twenty_plus", row[11]));
                    mysqlArgs.Add(new MySqlParameter("fourty_plus", row[12]));
                    mysqlArgs.Add(new MySqlParameter("lng", row[13]));
                    mysqlArgs.Add(new MySqlParameter("sck", row[14]));
                    mysqlArgs.Add(new MySqlParameter("scky", row[15]));

                    _db.ExecuteStoredProc(proc, mysqlArgs);
                }
            }
        }

        private static void UpdateDraftKings(string proc, string csv)
        {
            Console.WriteLine("Updating Draft Kings...");
            using (TextFieldParser parser = new TextFieldParser(csv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields();
                    var mysqlArgs = new List<MySqlParameter>();

                    var nameArr = row[0].Split(',');
                    row[0] = nameArr[1].Trim() + " " + nameArr[0].Trim();
                    mysqlArgs.Add(new MySqlParameter("player_name", row[0]));

                    mysqlArgs.Add(new MySqlParameter("position", row[1]));
                    mysqlArgs.Add(new MySqlParameter("year", row[2]));
                    mysqlArgs.Add(new MySqlParameter("week", row[3]));

                    row[4] = row[4].Replace("$", "").Replace(",", "").Replace(".00", "").Trim();
                    mysqlArgs.Add(new MySqlParameter("salary", row[4]));

                    mysqlArgs.Add(new MySqlParameter("fantasy_score", row[5]));
                    mysqlArgs.Add(new MySqlParameter("value_factor", row[6]));
                    mysqlArgs.Add(new MySqlParameter("dk_rank", row[7]));

                    _db.ExecuteStoredProc(proc, mysqlArgs);
                }
            }
        }
    }
}
