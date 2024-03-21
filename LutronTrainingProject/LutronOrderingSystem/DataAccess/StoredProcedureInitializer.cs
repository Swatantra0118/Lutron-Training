using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LutronOrderingSystem.DataAccess
{
    public class StoredProcedureInitializer
    {
        private readonly string connectionString;

        public StoredProcedureInitializer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void InitializeStoredProcedures()
        {
            try
            {
                CreateInsertProcedure();
                CreateSelectProcedure();
                CreateUpdateProcedure();
                CreateDeleteProcedure();

                Console.WriteLine("Stored procedures created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing stored procedures: {ex.Message}");
            }
        }

        private void CreateSelectProcedure()
        {
            string procedureDefinition = @"
            CREATE PROCEDURE GetProducts
            AS
            BEGIN
                SELECT * FROM Products;
            END
        ";
            ExecuteNonQuery(procedureDefinition);
        }
        private void CreateInsertProcedure()
        {
            string procedureDefinition = @"
            CREATE PROCEDURE AddProduct
                @ModelDisplayString NVARCHAR(100),
                @Description NVARCHAR(1000),
                @Category NVARCHAR(100),
                @NumberOfButtons INT = NULL,
                @MountType NVARCHAR(100) = NULL,
                @Quantity INT
            AS
            BEGIN
                SET NOCOUNT ON;

                IF @NumberOfButtons IS NULL
                    SET @NumberOfButtons = NULL;
        
                IF @MountType IS NULL
                    SET @MountType = NULL;

                INSERT INTO Products (ModelDisplayString, Description, Category, NumberOfButtons, MountType, Quantity)
                VALUES (@ModelDisplayString, @Description, @Category, @NumberOfButtons, @MountType, @Quantity);
            END
        ";

            ExecuteNonQuery(procedureDefinition);
        }
        private void CreateUpdateProcedure()
        {
            string procedureDefinition = @"
            CREATE PROCEDURE UpdateProduct
                @ModelId INT,
                @ModelDisplayString NVARCHAR(100),
                @Description NVARCHAR(1000),
                @Category NVARCHAR(100),
                @NumberOfButtons INT = NULL,
                @MountType NVARCHAR(100) = NULL,
                @Quantity INT
            AS
            BEGIN
                SET NOCOUNT ON;

                IF @NumberOfButtons IS NULL
                    SET @NumberOfButtons = NULL;
        
                IF @MountType IS NULL
                    SET @MountType = NULL;

                UPDATE Products 
                SET ModelDisplayString = @ModelDisplayString, 
                    Description = @Description, 
                    Category = @Category, 
                    NumberOfButtons = @NumberOfButtons, 
                    MountType = @MountType, 
                    Quantity = @Quantity 
                WHERE ModelId = @ModelId;

            END
        ";

            ExecuteNonQuery(procedureDefinition);
        }
        private void CreateDeleteProcedure()
        {
            string procedureDefinition = @"
            CREATE PROCEDURE DeleteProduct
                @ModelId INT
            AS
            BEGIN
                SET NOCOUNT ON;

                BEGIN TRY
                    BEGIN TRANSACTION;
        
                    DELETE FROM Products 
                    WHERE ModelId = @ModelId;
        
                    COMMIT TRANSACTION;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRANSACTION;
                    THROW;
                END CATCH
            END;
        ";

            ExecuteNonQuery(procedureDefinition);
        }
        private void ExecuteNonQuery(string procedureDefinition)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(procedureDefinition, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
