using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using WpfArasoi.Database;

namespace WpfArasoi.Model
{
    class Message
    {
        public static void CreateMessage(string subject, string message, string recipient, string championship_id = null)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                string query = @"INSERT INTO message
                (id, subject, message, author, recipient, championship_id)
                VALUES (UUID(), @subject, @message, @author,
                        @recipient, @championship_id)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@subject", subject);
                    command.Parameters.AddWithValue("@message", message);
                    command.Parameters.AddWithValue("@author", ActualUser.Id);
                    command.Parameters.AddWithValue("@recipient", recipient);

                    if (championship_id == null)
                        command.Parameters.AddWithValue("@championship_id", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@championship_id", championship_id);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Mensagem enviada!");
        }
    }

}