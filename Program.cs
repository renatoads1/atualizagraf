using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace AtualizaGraficoRobos2
{
    class Program
    {


        public static string connFb = "DataSource=192.168.0.21;Port=3050;Database=/banco/Questor.FDB; User=SYSDBA; Password=FPuZw9T@!0;Charset=ISO8859_1;Connection lifetime=0;Connection timeout=1;Pooling=false;";
        //public static string connMy = "server=192.168.0.27; port=3306; user=maturdb;password='m@ri@db!kgb';Database=RobosDapiGuiaTomados;";
        public static string connMy = "server=192.168.0.05; port=3306; user=root;password=;Database=RobosHomologacao;";
        public static string connFerra = "server=192.168.0.5; port=3306; user=root;password=;Database=Ferramentas;";

        static void Main(string[] args)
        {
            //using (var conexMy = new MySqlConnection(connMy))
            //{
            //    //string StrSqlMy = "truncate  RobosDapiGuiaTomados.BuzzGrafGer;";
            //    string StrSqlMy = "truncate  RobosHomologacao.BuzzMetas;";

            //    MySqlCommand comando = new MySqlCommand(StrSqlMy, conexMy);
            //    try
            //    {
            //        conexMy.Open();
            //        int count = Convert.ToInt32(comando.ExecuteScalar());
            //    }
            //    catch (Exception)
            //    {

            //        Console.WriteLine("Erro:"+StrSqlMy);
            //    }
                
            //}


            for (int i = 1; i < 13; i++)
            {
                DateTime datas = DateTime.Today;
                var primeiroDiaDoMes = new DateTime(datas.Year, i, 1).ToString("yyyy.MM.dd");
                var ultimoDiaDoMes = new DateTime(datas.Year, i, DateTime.DaysInMonth(datas.Year, i)).ToString("yyyy.MM.dd");
                Console.WriteLine(primeiroDiaDoMes + "   -    " + ultimoDiaDoMes+" i="+i);
                string queryUsers = "select count(CHAVELCTOFISENT) from lctofisent where codigoempresa between '1' and '9999' and datalctofis between  '"+ primeiroDiaDoMes + "' and '"+ ultimoDiaDoMes + "' and codigousuario =477";
                //usuarios

                using (FbConnection dbConn2 = new FbConnection(connFb))
                {
                    FbCommand myCommand2 = new FbCommand(queryUsers, dbConn2);
                    dbConn2.Open();
                    myCommand2.CommandTimeout = 0;
                    var myReaderUser = myCommand2.ExecuteReader();
                    while (myReaderUser.Read())
                    {
                        Console.WriteLine(myReaderUser.GetInt32(0));
                        Console.WriteLine("_______________________________________________________");

                        using (var conexMy = new MySqlConnection(connMy))
                        {
                            string StrSqlMy = "update RobosHomologacao.BuzzMetas set BuzzQuantidade = '" + myReaderUser.GetInt32(0) + "' , BuzzMes = '"+i+ "' , BuzzRobo = '2', BuzzDataIncercao = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "'  where id = '" + i+ "' ";

                            MySqlCommand comando = new MySqlCommand(StrSqlMy, conexMy);
                            conexMy.Open();
                            int count = Convert.ToInt32(comando.ExecuteScalar());

                        }
                    }
                }
            }

            for (int i = 3; i < 15; i++)
            {
                int mes = (i - 2);
                int idl = (i + 10);

                DateTime datas = DateTime.Today;
                var primeiroDiaDoMes = new DateTime(datas.Year, mes, 1).ToString("yyyy.MM.dd");
                var ultimoDiaDoMes = new DateTime(datas.Year, mes, DateTime.DaysInMonth(datas.Year, mes)).ToString("yyyy.MM.dd");
                Console.WriteLine(primeiroDiaDoMes + "   -    " + ultimoDiaDoMes + " i= " + i+" mes= "+ mes+ " idl= "+ idl);
                string queryUsers = "select count(CHAVELCTOFISsai) from lctofissai where codigoempresa between '1' and '9999' and datalctofis between  '" + primeiroDiaDoMes + "' and '" + ultimoDiaDoMes + "' and codigousuario =477";
                //usuarios

                using (FbConnection dbConn2 = new FbConnection(connFb))
                {
                    FbCommand myCommand2 = new FbCommand(queryUsers, dbConn2);
                    dbConn2.Open();
                    myCommand2.CommandTimeout = 0;
                    var myReaderUser = myCommand2.ExecuteReader();
                    while (myReaderUser.Read())
                    {
                        Console.WriteLine(myReaderUser.GetInt32(0));
                        Console.WriteLine("_______________________________________________________");

                        using (var conexMy = new MySqlConnection(connMy))
                        {
                            string StrSqlMy = "update RobosHomologacao.BuzzMetas set BuzzQuantidade = '" + myReaderUser.GetInt32(0) + "' , BuzzMes = '"+mes+ "' , BuzzRobo = '7' , BuzzDataIncercao = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "' where id = " + idl + " ";
                            //string StrSqlMy = "INSERT INTO RobosHomologacao.BuzzMetas (BuzzQuantidade,BuzzMes,BuzzRobo) VALUES('" + myReaderUser.GetInt32(0) + "','" + i + "','7')";

                            MySqlCommand comando = new MySqlCommand(StrSqlMy, conexMy);
                            conexMy.Open();
                            int count = Convert.ToInt32(comando.ExecuteScalar());
                        }


                    }
                }

            }

            using (var conexMy = new MySqlConnection(connFerra))
            {
                string StrSqlMy = "truncate  Ferramentas.EstabQuestor;";

                MySqlCommand comando = new MySqlCommand(StrSqlMy, conexMy);
                conexMy.Open();
                int count = Convert.ToInt32(comando.ExecuteScalar());
            }

            string queryEstabQuestor = "SELECT CODIGOEMPRESA, CODIGOESTAB, DATAINICIAL, NOMEESTAB, NOMEESTABCOMPLETO, NOMEFANTASIA, APELIDOESTAB, CODIGOTIPOLOGRAD, ENDERECOESTAB, NUMENDERESTAB, COMPLENDERESTAB, BAIRROENDERESTAB, DATAALTERENDER, SIGLAESTADO, CODIGOMUNIC, CEPENDERESTAB, DDDFONE, NUMEROFONE, DDDFAX, NUMEROFAX, CAIXAPOSTAL, SIGLAESTADOCXP, CEPCAIXAPOSTAL, EMAIL, PAGINAINTERNET, DATAINICIOATIV, DATAENCERATIV, SOCIEDADECONTAPARTICIPACAO, INSCRICAOSCP, TIPOINSCR, INSCRFEDERAL, CPFRESPCNO, SUFRAMA, CODIGONATURJURID, CODIGOATIVFEDERAL, DESCRATIVFEDESTAB, DATAALTERATIVFED, TIPOREGIST, NUMEROREGIST, DATAREGIST, OBSERVREGIST, INSCRESTAD, CODIGOATIVESTAD,  DESCRATIVESTESTAB, INSCRMUNIC, CODIGOATIVMUNIC, INSCRIMOBILIARIA, ESPECIEESTAB, INSCRBANCOCENTRAL, INSCRSUSEP, DESCRATIVMUNESTAB, PORTEEMPRESA, INSCRCVM, CODIGOTABFERIADO, VALORNOMINALCOTAS, CAEPF, NOMEAUDITOR, CVMAUDITOR, INSCRFEDERALAUDITOR, CAPITALSOCIAL, INSCRCAEPF, INSCRFEDERALPRODRURAL, CODIGOCATEGEMPRESACLIENTE, CERTIFICADO, STATUSSINCZEN, TIPOALTERACAOESOCIAL FROM ESTAB ;";
            using (FbConnection dbConnQuest = new FbConnection(connFb))
            {
                FbCommand myCommandQuest = new FbCommand(queryEstabQuestor, dbConnQuest);
                dbConnQuest.Open();
                myCommandQuest.CommandTimeout = 0;
                var myReaderQuest = myCommandQuest.ExecuteReader();
                while (myReaderQuest.Read())
                {
                    using (var conexMy = new MySqlConnection(connFerra))
                    {
                        string StrSqlMy = " INSERT INTO FerramentasHomologacao.EstabQuestor ( CodigoEmpresa, CodigoEstab, DataInicial, NomeEstab," +
                            " NomeEstabCompleto, NomeFantasia, ApelidoEstab, CodigoTipoLograd, EnderecoEstab, NumEnderEstab, ComplEnderEstab," +
                            " BairroEnderEstab, DataAltErender, SiglaEstado, CodigoMunic, CepEnderEstab, DddFone, NumeroFone, DddFax," +
                            " NumeroFax, CaixaPostal, SiglaEstaDocxp, CepCaixaPostal, Email, PaginaInternet, DataInicioAtiv," +
                            " DataEncerAtiv, SociedadeContaParticipacao, InscricaoScp, TipoInscr, InscrFederal, CpfRespcno," +
                            " Suframa, CodigoNaturJurid, CodigoAtivFederal, DescrativFedEstab, DataAlterAtivFed, TipoRegist," +
                            " NumeroRegist, DataRegist, ObservRegist, InscrEstad, CodigoAtivEstad, DescrAtivEstestab," +
                            " InscrMunic, CodigoAtivMunic, InscrImobiliaria, EspecieEstab, InscrBancoCentral, InscrSusep," +
                            " DescrAtivMuneEtab, PorteEmpresa, InscrCvm, CodigoTabFeriado, ValorNominalCotas, CaePf," +
                            " NomeAuditor, CvmAuditor, InscrFederalAuditor, CapitalSocial, InscrCaepf, InscrFederalProdRural," +
                            " CodigoCategEmpresaCliente, Certificado, StatusCinczen, TipoAlteracaoEsocial) VALUES" +
                            "(" + myReaderQuest["CODIGOEMPRESA"].ToString().Replace(@"'", " ") + ", " +
                            "" + myReaderQuest["CODIGOESTAB"].ToString().Replace(@"'", " ") + "," +
                            "'" + myReaderQuest["DATAINICIAL"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["NOMEESTAB"].ToString().Replace(@"'", " ").Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["NOMEESTABCOMPLETO"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["NOMEFANTASIA"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["APELIDOESTAB"].ToString().Replace(@"'", " ") + "'," +
                            myReaderQuest["CODIGOTIPOLOGRAD"].ToString().Replace(@"'", " ") + "," +
                            "'" + myReaderQuest["ENDERECOESTAB"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["NUMENDERESTAB"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["COMPLENDERESTAB"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["BAIRROENDERESTAB"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["DATAALTERENDER"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["SIGLAESTADO"].ToString().Replace(@"'", " ") + "'," +
                            myReaderQuest["CODIGOMUNIC"].ToString().Replace(@"'", " ") + "," +
                            "'" + myReaderQuest["CEPENDERESTAB"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["DDDFONE"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["NUMEROFONE"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["DDDFAX"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["NUMEROFAX"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["CAIXAPOSTAL"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["SIGLAESTADOCXP"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["CEPCAIXAPOSTAL"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["EMAIL"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["PAGINAINTERNET"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["DATAINICIOATIV"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["DATAENCERATIV"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["SOCIEDADECONTAPARTICIPACAO"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["INSCRICAOSCP"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["TIPOINSCR"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["INSCRFEDERAL"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["CPFRESPCNO"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["SUFRAMA"].ToString().Replace(@"'", " ") + "'," +
                            myReaderQuest["CODIGONATURJURID"].ToString().Replace(@"'", " ") + "," +
                            "'" + myReaderQuest["CODIGOATIVFEDERAL"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["DESCRATIVFEDESTAB"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["DATAALTERATIVFED"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["TIPOREGIST"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["NUMEROREGIST"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["DATAREGIST"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["OBSERVREGIST"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["INSCRESTAD"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["CODIGOATIVESTAD"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["DESCRATIVESTESTAB"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["INSCRMUNIC"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["CODIGOATIVMUNIC"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["INSCRIMOBILIARIA"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["ESPECIEESTAB"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["INSCRBANCOCENTRAL"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["INSCRSUSEP"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["DESCRATIVMUNESTAB"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["PORTEEMPRESA"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["INSCRCVM"].ToString().Replace(@"'", " ") + "'," +
                            myReaderQuest["CODIGOTABFERIADO"].ToString().Replace(@"'", " ") + "," +
                            "'" + myReaderQuest["VALORNOMINALCOTAS"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["CAEPF"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["NOMEAUDITOR"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["CVMAUDITOR"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["INSCRFEDERALAUDITOR"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["CAPITALSOCIAL"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["INSCRCAEPF"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["INSCRFEDERALPRODRURAL"].ToString().Replace(@"'", " ") + "','" +
                            myReaderQuest["CODIGOCATEGEMPRESACLIENTE"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["CERTIFICADO"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["STATUSSINCZEN"].ToString().Replace(@"'", " ") + "'," +
                            "'" + myReaderQuest["TIPOALTERACAOESOCIAL"].ToString().Replace(@"'", " ") + "');";

                        MySqlCommand comando = new MySqlCommand(StrSqlMy, conexMy);
                        try
                        {
                            conexMy.Open();
                            Console.WriteLine("Runing Empresas");
                            int count = Convert.ToInt32(comando.ExecuteScalar());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine(StrSqlMy);
                        }

                    }


                }
            }


        }
    }
}



