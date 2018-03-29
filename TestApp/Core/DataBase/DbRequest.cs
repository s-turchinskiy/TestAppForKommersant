using System;
using System.Data;
using System.Data.SqlClient;

namespace TestApp
{
    internal class DbRequest : IDbRequest
    {
        private readonly int mMaxAge;
        private IDbConnection mConnection;
        public DbRequest(int maxAge)
        {
            mMaxAge = maxAge;
        }

        public IDataReader OpenConnectionAndRequestData()
        {
            String stringConnection = "Persist Security Info=False;User ID=s.turchinskiy;Pwd=s.turchinskiy;data source=DEVDB-V;initial catalog=Employees";
            SqlConnection connection = new SqlConnection(stringConnection);

            mConnection = new SqlConnection(Properties.Settings.Default.DbConnect);
            string SqlRequest = @"IF OBJECT_ID('tempdb.dbo.#Car') IS NOT NULL
	            DROP TABLE #Car
                ;

                DECLARE 
                @Cars VARCHAR(1000)

                SELECT 
                c.OwnerID
                , c.ModelName
                , RowNum = ROW_NUMBER() OVER (PARTITION BY OwnerID ORDER BY Year,ModelName)
                , Cars = CAST(NULL AS VARCHAR(1000))
	            , Year
                INTO #CAR
                FROM dbo.Car c inner join dbo.People p
                    on c.OwnerId = p.Id
                Where (DATEADD(Year,-1*@MaxAge,GETDATE())<=p.BirthDate or @MaxAge=0)
                            


                update #car
                set 
                    @Cars = Cars =
                    case when rownum = 1 
                    then rtrim(modelname) + ' (' + Cast(year as varchar(4)) + ')'
                    else @Cars + ', ' + rtrim(modelname) + ' (' + Cast(year as varchar(4)) + ')'
                end

                select
	                p.Id
	                , rtrim(p.Name)
	                , CASE 
		                WHEN MONTH(GETDATE()) >= MONTH(p.BirthDate) AND DAY(GETDATE()) >= DAY(p.BirthDate) THEN 
		                YEAR(GETDATE()) - YEAR(p.BirthDate) 
		                ELSE (YEAR(GETDATE()) - YEAR(p.BirthDate) - 1) 
		                END  As Age
                        , concat(max(Cars),'') as Cars
                            from dbo.People p left join #car c
                            on p.Id = c.OwnerId
                            Where (DATEADD(Year,-1*@MaxAge,GETDATE())<=p.BirthDate or @MaxAge=0)
                            group by c.ownerid,p.Name,p.BirthDate, p.Id
                            order by p.BirthDate Desc";
            IDbCommand command = new SqlCommand(SqlRequest);
            SqlParameter sqlParameterMaxAge = new SqlParameter("@MaxAge", SqlDbType.VarChar) { Value = mMaxAge };
            command.Parameters.Add(sqlParameterMaxAge);
            command.Connection = mConnection;
            mConnection.Open();
            return command.ExecuteReader();
        }

        public void CloseConnection()
        {
            if (mConnection != null && mConnection.State != ConnectionState.Closed)
            {
                mConnection.Dispose();
            }
        }

        #region IDisposable Support
        private bool mDisposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!mDisposedValue)
            {
                if (disposing)
                {
                    CloseConnection();
                }
                mDisposedValue = true;
            }
        }

        ~DbRequest()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
