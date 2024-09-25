using MySql.Data.MySqlClient;
using Task1QQQ.Models;

namespace Task1QQQ
{
    public class DbService
    {
        private string connectionString = "server=localhost;user=root;database=mydb;port=3506;password=12345678";

        public List<Substance> GetSubstances(
         string name = null,
         int? density = null,
         int? calorificValue = null,
         int? minConcentration = null,
         int? maxConcentration = null,
         int? substanceTypeId = null)
        {
            List<Substance> substances = new List<Substance>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Substance WHERE 1=1";

                if (name != null)
                    query += " AND name LIKE @name";
                if (density.HasValue)
                    query += " AND density = @density";
                if (calorificValue.HasValue)
                    query += " AND calorific_value = @calorificValue";
                if (minConcentration.HasValue)
                    query += " AND min_concentration = @minConcentration";
                if (maxConcentration.HasValue)
                    query += " AND max_concentration = @maxConcentration";
                if (substanceTypeId.HasValue)
                    query += " AND Substance_type_id_Substance_type = @substanceTypeId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (name != null)
                        cmd.Parameters.AddWithValue("@name", "%" + name + "%");
                    if (density.HasValue)
                        cmd.Parameters.AddWithValue("@density", density);
                    if (calorificValue.HasValue)
                        cmd.Parameters.AddWithValue("@calorificValue", calorificValue);
                    if (minConcentration.HasValue)
                        cmd.Parameters.AddWithValue("@minConcentration", minConcentration);
                    if (maxConcentration.HasValue)
                        cmd.Parameters.AddWithValue("@maxConcentration", maxConcentration);
                    if (substanceTypeId.HasValue)
                        cmd.Parameters.AddWithValue("@substanceTypeId", substanceTypeId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            substances.Add(new Substance
                            {
                                Id = reader.GetInt32("id_Substance"),
                                Name = reader.GetString("name"),
                                Density = reader.GetInt32("density"),
                                CalorificValue = reader.GetInt32("calorific_value"),
                                MinConcentration = reader.GetInt32("min_concentration"),
                                MaxConcentration = reader.GetInt32("max_concentration"),
                                SubstanceTypeId = reader.GetInt32("Substance_type_id_Substance_type")
                            });
                        }
                    }
                }
            }

            return substances;
        }

        public List<SubstanceType> GetSubstanceTypes()
        {
            List<SubstanceType> substanceTypes = new List<SubstanceType>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Substance_type";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            substanceTypes.Add(new SubstanceType
                            {
                                Id = reader.GetInt32("id_Substance_type"),
                                Name = reader.GetString("s_t_name")
                            });
                        }
                    }
                }
            }

            return substanceTypes;
        }


        public bool AddSubstance(string name, int density, int calorificValue, int minConcentration, int maxConcentration, int substanceTypeId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Substance (name, density, calorific_value, min_concentration, max_concentration, Substance_type_id_Substance_type)
                             VALUES (@name, @density, @calorificValue, @minConcentration, @maxConcentration, @substanceTypeId)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@density", density);
                    cmd.Parameters.AddWithValue("@calorificValue", calorificValue);
                    cmd.Parameters.AddWithValue("@minConcentration", minConcentration);
                    cmd.Parameters.AddWithValue("@maxConcentration", maxConcentration);
                    cmd.Parameters.AddWithValue("@substanceTypeId", substanceTypeId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool AddSubstanceType(string name)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "INSERT INTO Substance_type (s_t_name) VALUES (@name)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool DeleteSubstance(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "DELETE FROM Substance WHERE id_Substance = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteSubstanceType(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "DELETE FROM Substance_type WHERE id_Substance_type = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
