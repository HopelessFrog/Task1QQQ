using MySql.Data.MySqlClient;
using System.Windows;
using Task1QQQ.Models;

namespace Task1QQQ
{
    public class DbService
    {
        private string connectionString = "server=localhost;user=root;database=mydb;port=3506;password=12345678";

        public List<Substance> FindSubstances(string name = null, decimal? minDensity = null, decimal? maxDensity = null,
                                           decimal? minCalorificValue = null, decimal? maxCalorificValue = null,
                                           int? substanceTypeId = null, bool density = false, bool calorific = false)
        {
            List<Substance> substances = new List<Substance>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно подключится к бд");
                    return new();
                }
                MySqlCommand cmd = conn.CreateCommand();


                List<string> conditions = new List<string>();

                if (!string.IsNullOrEmpty(name))
                    conditions.Add("s.name LIKE @name");
                if (minDensity.HasValue && density)
                    conditions.Add("s.density >= @minDensity");
                if (maxDensity.HasValue && density)
                    conditions.Add("s.density <= @maxDensity");
                if (minCalorificValue.HasValue && calorific)
                    conditions.Add("s.calorific_value >= @minCalorificValue");
                if (maxCalorificValue.HasValue && calorific)
                    conditions.Add("s.calorific_value <= @maxCalorificValue");
                if (substanceTypeId.HasValue && substanceTypeId != 0)
                    conditions.Add("s.Substance_type_id = @substanceTypeId");

                string query = @"SELECT s.id_Substance, s.name, s.density, s.calorific_value, s.min_concentration, s.max_concentration, 
                                    st.id_Substance_type, st.s_t_name
                             FROM Substance s
                             JOIN Substance_type st ON s.Substance_type_id = st.id_Substance_type";

                if (conditions.Count > 0)
                    query += " WHERE " + string.Join(" AND ", conditions);

                cmd.CommandText = query;

                if (!string.IsNullOrWhiteSpace(name))
                    cmd.Parameters.AddWithValue("@name", $"%{name}%");
                if (minDensity.HasValue && density)
                    cmd.Parameters.AddWithValue("@minDensity", minDensity);
                if (maxDensity.HasValue && density)
                    cmd.Parameters.AddWithValue("@maxDensity", maxDensity);
                if (minCalorificValue.HasValue && calorific)
                    cmd.Parameters.AddWithValue("@minCalorificValue", minCalorificValue);
                if (maxCalorificValue.HasValue && calorific)
                    cmd.Parameters.AddWithValue("@maxCalorificValue", maxCalorificValue);
                if (substanceTypeId.HasValue && substanceTypeId != 0)
                    cmd.Parameters.AddWithValue("@substanceTypeId", substanceTypeId);


                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        SubstanceType substanceType = new SubstanceType
                        {
                            Id = reader.GetInt32("id_Substance_type"),
                            Name = reader.GetString("s_t_name")
                        };


                        substances.Add(new Substance
                        {
                            Id = reader.GetInt32("id_Substance"),
                            Name = reader.GetString("name"),
                            Density = reader.GetDecimal("density"),
                            CalorificValue = reader.GetDecimal("calorific_value"),
                            MinConcentration = reader.GetInt32("min_concentration"),
                            MaxConcentration = reader.GetInt32("max_concentration"),
                            SubstanceType = substanceType
                        });
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
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    return new();
                }

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


        public bool AddSubstance(string name, decimal density, decimal calorificValue, int minConcentration, int maxConcentration, int substanceTypeId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    return false;
                }

                string query = @"INSERT INTO Substance (name, density, calorific_value, min_concentration, max_concentration, Substance_type_id)
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
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    return false;
                }

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
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    return false;
                }

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
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    return false;
                }

                string query = "DELETE FROM Substance_type WHERE id_Substance_type = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateSubstance(int id, string name, decimal density, decimal calorificValue, int minConcentration, int maxConcentration, int substanceTypeId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    return false;
                }

                string query = @"UPDATE Substance 
                         SET name = @name, 
                             density = @density, 
                             calorific_value = @calorificValue, 
                             min_concentration = @minConcentration, 
                             max_concentration = @maxConcentration, 
                             Substance_type_id = @substanceTypeId 
                         WHERE id_Substance = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
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
    }
}
