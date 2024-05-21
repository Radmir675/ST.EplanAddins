using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ST.EplAddin.Comments
{
    class CommentInsert : IEplAction
    {
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            CommentInsertForm Form = new CommentInsertForm();
            Form.ShowDialog();
            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "CommentInsertDialog";
            Ordinal = 20;
            return true;
        }


        public static void InsertComment(String text, String user, int status)
        {
            bool chkCommentTextGroup = true;

            //Слой (A411 = 519(EPLAN519, Grafik.Kommentare))
            string sLayer = "519";

            //Тип линии (A412 = L(Layer) / 0(durchgezogen) / 41(~~~~~))
            string sLinetype = "0";

            //Длина (A415 = L(Layer) / -1.5(1,50 mm) / -32(32,00 mm))
            string sLineWidth = "-1.5";

            //Цвет (A413 = 0(schwarz) / 1(красный) / 2(Желтый) / 3(светложеленый) / 4(светлосиний) / 5(темносиний) / 6(фиолетовый) / 8(белый) / 40(оранжвый))
            string sColor = "40";

            //Автор (A2521)
            string sAuthor = user;

            //Дата создания (A2524)
            string sDateOfCreation = ""; // DateTimeToUnixTimestamp(dTPErstellungsdatum.Value).ToString(); // DateTime Wert nach Unix Timestamp Format wandeln

            //Текст комментария (A511)
            string sCommentText = text;
            if (sCommentText.EndsWith(Environment.NewLine)) //Kommentar darf nicht mit Zeilenumbruch enden
            {
                sCommentText = sCommentText.Substring(0, sCommentText.Length - 2);
            }
            sCommentText = sCommentText.Replace(Environment.NewLine, "&#10;"); //Kommentar Zeilenumbruch umwandeln
            sCommentText = "??_??@" + sCommentText; //Kommentar MultiLanguage String

            if (!sCommentText.EndsWith(";")) //Kommentar muss mit ";" enden
            {
                sCommentText += ";";
            }

            //Status, (A2527 = 0(Без статуса) / 1(Принято) / 2(Отклонено) / 3(Отменено) / 4(Завершено))
            string sStatus = status.ToString();

            //Путь к временному файлу
            string sTempFile;

            sTempFile = PathMap.SubstitutePath(@"$(TMP)") + @"\tmpInsertComment.ema";

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(sTempFile, settings);

            //Содержимое макроса
            writer.WriteRaw("\n<EplanPxfRoot Name=\"#Kommentar\" Type=\"WindowMacro\" Version=\"2.2.6360\" PxfVersion=\"1.23\" SchemaVersion=\"1.7.6360\" Source=\"\" SourceProject=\"\" Description=\"\" ConfigurationFlags=\"0\" NumMainObjects=\"0\" NumProjectSteps=\"0\" NumMDSteps=\"0\" Custompagescaleused=\"true\" StreamSchema=\"EBitmap,BaseTypes,2,1,2;EPosition3D,BaseTypes,0,3,2;ERay3D,BaseTypes,0,4,2;EStreamableVector,BaseTypes,0,5,2;DMNCDataSet,TrDMProject,1,20,2;DMNCDataSetVector,TrDMProject,1,21,2;DMPlaceHolderRuleData,TrDMProject,0,22,2;Arc3d@W3D,W3dBaseGeometry,0,36,2;Box3d@W3D,W3dBaseGeometry,0,37,2;Circle3d@W3D,W3dBaseGeometry,0,38,2;Color@W3D,W3dBaseGeometry,0,39,2;ContourPlane3d@W3D,W3dBaseGeometry,0,40,2;CTexture@W3D,W3dBaseGeometry,0,41,2;CTextureMap@W3D,W3dBaseGeometry,0,42,2;Line3d@W3D,W3dBaseGeometry,0,43,2;Linetype@W3D,W3dBaseGeometry,0,44,2;Material@W3D,W3dBaseGeometry,3,45,2;Path3d@W3D,W3dBaseGeometry,0,46,2;Mesh3dX@W3D,W3dMeshModeller,2,47,2;MeshBox@W3D,W3dMeshModeller,5,48,2;MeshMate@W3D,W3dMeshModeller,7,49,2;MeshMateFace@W3D,W3dMeshModeller,1,50,2;MeshMateGrid@W3D,W3dMeshModeller,8,51,2;MeshMateGridLine@W3D,W3dMeshModeller,1,52,2;MeshMateLine@W3D,W3dMeshModeller,1,53,2;MeshText3dX@W3D,W3dMeshModeller,0,55,2;BaseTextLine@W3D,W3dMeshModeller,2,56,2;Mesh3d@W3D,W3dMeshModeller,8,57,2;MeshEdge3d@W3D,W3dMeshModeller,0,58,2;MeshFace3d@W3D,W3dMeshModeller,2,59,2;MeshPoint3d@W3D,W3dMeshModeller,1,60,2;MeshPolygon3d@W3D,W3dMeshModeller,1,61,2;MeshSimpleTextureTriangle3d@W3D,W3dMeshModeller,2,62,2;MeshSimpleTriangle3d@W3D,W3dMeshModeller,1,63,2;MeshTriangle3d@W3D,W3dMeshModeller,2,64,2;MeshTriangleFace3d@W3D,W3dMeshModeller,0,65,2;MeshTriangleFaceEdge3D@W3D,W3dMeshModeller,0,66,2\">");
            writer.WriteRaw("\n <MacroVariant MacroFuncType=\"1\" VariantId=\"0\" ReferencePoint=\"64/248/0\" Version=\"2.2.6360\" PxfVersion=\"1.23\" SchemaVersion=\"1.7.6360\" Source=\"\" SourceProject=\"\" Description=\"\" ConfigurationFlags=\"0\" DocumentType=\"1\" Customgost=\"0\">");
            writer.WriteRaw("\n  <O4 Build=\"6360\" A1=\"4/18\" A3=\"0\" A13=\"0\" A14=\"0\" A47=\"1\" A48=\"1362057551\" A50=\"1\" A59=\"1\" A404=\"1\" A405=\"64\" A406=\"0\" A407=\"0\" A431=\"1\" A1101=\"17\" A1102=\"\" A1103=\"\">");

            //Группа - начало
            if (chkCommentTextGroup == true)
                writer.WriteRaw("\n  <O26 Build=\"6360\" A1=\"26/128740\" A3=\"0\" A13=\"0\" A14=\"0\" A404=\"1\" A405=\"64\" A406=\"0\" A407=\"0\" A431=\"1\">");

            //Свойства комментария
            writer.WriteRaw("\n  <O165 Build=\"6360\" A1=\"165/128741\" A3=\"0\" A13=\"0\" A14=\"0\" A404=\"33\" A405=\"64\" A406=\"0\" A407=\"0\" A411=\"" + sLayer +
                "\" A412=\"" + sLinetype +
                "\" A413=\"" + sColor + "\" A414=\"0.352777238812552\" A415=\"" + sLineWidth + "\" A416=\"0\" A501=\"64/248\" A503=\"0\" A504=\"0\" A506=\"22\" A511=\"" + sCommentText +
                "\" A2521=\"" + sAuthor + "\" A2522=\"\" A2523=\"\" A2524=\"" + sDateOfCreation +
                "\" A2525=\"" + sDateOfCreation + "\" A2526=\"2\" A2527=\"" + sStatus + "\" A2528=\"0\" A2529=\"0\" A2531=\"0\" A2532=\"0\" A2533=\"64/248;70.349110320284/254.349110320285\" A2534=\"2\" A2539=\"0\" A2540=\"0\">");
            writer.WriteRaw("\n  <S54x505 A961=\"L\" A962=\"L\" A963=\"0\" A964=\"L\" A965=\"0\" A966=\"0\" A967=\"0\" A968=\"0\" A969=\"0\" A4000=\"L\" A4001=\"L\" A4013=\"0\"/>");
            writer.WriteRaw("\n  </O165>");

            //Свойства Text
            writer.WriteRaw("\n  <O30 Build=\"6360\" A1=\"30/128742\" A3=\"0\" A13=\"0\" A14=\"0\" A404=\"33\" A405=\"64\" A406=\"0\" A407=\"0\" A411=\"" + sLayer +
                "\" A412=\"L\" A413=\"L\" A414=\"L\" A415=\"L\" A416=\"0\" A501=\"71/255\" A503=\"0\" A504=\"0\" A506=\"0\" A511=\"" + sCommentText + "\">");

            //O165 Комментарий
            //O30 Текст
            //O26 Группа

            //A501 Позиция X/Y
            //A404 Активировать блок выравнивания FALSE-1 - TRUE-33
            //A411 Слой
            //A412 Тип линии
            //A413 Цвет
            //A967 Блок выравнивания - Ширина
            //A968 Блок выравнивания - Высота

            //A1651 = "0/-5" Height
            //A1652 = "5/0" Width

            //A2521 Автор
            //A2524 Дата создания

            //writer.WriteRaw("\n  <S54x505 A961=\"L\" A962=\"L\" A963=\"0\" A964=\"L\" A965=\"0\" A966=\"0\" A967=\"0\" A968=\"0\" A969=\"0\" A4000=\"L\" A4001=\"L\" A4013=\"0\"/>");
            writer.WriteRaw("\n  <S54x505 A961=\"L\" A962=\"0\" A963=\"278\" A964=\"L\" A965=\"16\" A966=\"1\" A967=\"50\" A968=\"20\" A969=\"64\" A4000=\"L\" A4001=\"L\" A4013=\"0\"/>");
            writer.WriteRaw("\n  </O30>");

            //Группа - конец
            if (chkCommentTextGroup == true)
                writer.WriteRaw("\n  </O26>");

            writer.WriteRaw("\n  <O37 Build=\"6360\" A1=\"37/128743\" A3=\"1\" A13=\"0\" A14=\"0\" A404=\"1\" A405=\"64\" A406=\"0\" A407=\"0\" A682=\"1\" A683=\"26/128740\" A684=\"0\" A687=\"8\" A688=\"2\" A689=\"-1\" A690=\"-1\" A691=\"0\" A693=\"1\" A792=\"0\" A793=\"0\" A794=\"0\" A1261=\"0\" A1262=\"44\" A1263=\"0\" A1631=\"0/-8\" A1632=\"8/0\">");
            writer.WriteRaw("\n  <S109x692 Build=\"6360\" A3=\"0\" A13=\"0\" A14=\"0\" R1906=\"165/128741\"/>");
            writer.WriteRaw("\n  <S109x692 Build=\"6360\" A3=\"0\" A13=\"0\" A14=\"0\" R1906=\"30/128742\"/>");
            writer.WriteRaw("\n  <S40x1201 A762=\"64/254.349110320285\">");
            writer.WriteRaw("\n  <S39x761 A751=\"1\" A752=\"0\" A753=\"0\" A754=\"1\"/>");
            writer.WriteRaw("\n  </S40x1201>");
            writer.WriteRaw("\n  <S89x5 Build=\"6360\" A3=\"0\" A4=\"1\" R7=\"37/128743\" A13=\"0\" A14=\"0\" A404=\"9\" A405=\"64\" A406=\"0\" A407=\"0\" A411=\"308\" A412=\"L\" A413=\"L\" A414=\"L\" A415=\"L\" A416=\"0\" A1651=\"0/-8\" A1652=\"8/0\" A1653=\"0\" A1654=\"0\" A1655=\"0\" A1656=\"0\" A1657=\"0\"/>");
            writer.WriteRaw("\n  </O37>");
            writer.WriteRaw("\n  </O4>");
            writer.WriteRaw("\n </MacroVariant>");
            writer.WriteRaw("\n</EplanPxfRoot>");

            // Write the XML to file and close the writer.
            writer.Flush();
            writer.Close();

            //Вставить макрос

            CommandLineInterpreter oCli = new CommandLineInterpreter();
            ActionCallingContext oAcc = new ActionCallingContext();
            oAcc.AddParameter("Name", "XMIaInsertMacro");
            oAcc.AddParameter("filename", sTempFile);
            oAcc.AddParameter("variant", "0");
            oCli.Execute("XGedStartInteractionAction", oAcc);

            //   Close();
            return;
        }

        public static void InsertCommentNew(String text, String user, int status)
        {
            bool chkCommentTextGroup = true;

            //Тип линии (A412 = L(Layer) / 0(durchgezogen) / 41(~~~~~))
            string sLinetype = "0";

            //Длина (A415 = L(Layer) / -1.5(1,50 mm) / -32(32,00 mm))
            string sLineWidth = "-1.5";

            //Цвет (A413 = 0(schwarz) / 1(красный) / 2(Желтый) / 3(светложеленый) / 4(светлосиний) / 5(темносиний) / 6(фиолетовый) / 8(белый) / 40(оранжвый))
            string sColor = "40";

            //Автор (A2521)
            string sAuthor = user;

            //Дата создания (A2524)
            string sDateOfCreation = ""; // DateTimeToUnixTimestamp(dTPErstellungsdatum.Value).ToString(); // DateTime Wert nach Unix Timestamp Format wandeln

            //Текст комментария (A511)
            string sCommentIcon = " ";
            string sCommentText = text;
            if (sCommentText.EndsWith(Environment.NewLine)) //Kommentar darf nicht mit Zeilenumbruch enden
            {
                sCommentText = sCommentText.Substring(0, sCommentText.Length - 2);
            }
            sCommentText = sCommentText.Replace(Environment.NewLine, "&#10;"); //Kommentar Zeilenumbruch umwandeln
            sCommentText = "??_??@" + sCommentText; //Kommentar MultiLanguage String

            if (!sCommentText.EndsWith(";")) //Kommentar muss mit ";" enden
            {
                sCommentText += ";";
            }

            //Status, (A2527 = 0(Без статуса) / 1(Принято) / 2(Отклонено) / 3(Отменено) / 4(Завершено))
            string sStatus = status.ToString();

            //Путь к временному файлу
            string sTempFile;

            sTempFile = PathMap.SubstitutePath(@"$(TMP)") + @"\comment.ema";

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(sTempFile, settings);


            //block
            /* writer.WriteRaw($"\n<EplanPxfRoot Name =\"comment4\" Type=\"WindowMacro\" Version=\"2.8.12959\" PxfVersion=\"1.23\" SchemaVersion=\"1.7.12959\" Source=\"\" SourceProject=\"\" Description=\"\" ConfigurationFlags=\"8193\" NumMainObjects=\"1\" NumProjectSteps=\"1\" NumMDSteps=\"0\" Custompagescaleused=\"true\" StreamSchema=\"EBitmap, ,2,1,2;EPosition3D, ,0,3,2;ERay3D, ,0,4,2;EStreamableVector, ,0,5,2;BaseTextLine@W3D, ,2,22,2;Box3d@W3D, ,0,23,2;DMNCDataSet, ,1,25,2;DMNCDataSetVector, ,1,26,2;Linetype@W3D, ,0,28,2;Color@W3D, ,0,29,2;Material@W3D, ,3,30,2;Mesh3d@W3D, ,8,32,2;Mesh3dX@W3D, ,2,33,2;MeshBox@W3D, ,5,34,2;MeshEdge3d@W3D, ,0,35,2;MeshFace3d@W3D, ,2,36,2;MeshPoint3d@W3D, ,1,37,2;MeshPolygon3d@W3D, ,1,38,2;MeshSimpleTextureTriangle3d@W3D, ,2,39,2;MeshSimpleTriangle3d@W3D, ,1,40,2;MeshText3dX@W3D, ,0,41,2;MeshTriangle3d@W3D, ,2,42,2;MeshTriangleFace3d@W3D, ,0,43,2;MeshTriangleFaceEdge3D@W3D, ,0,44,2;CTexture@W3D,W3dBaseGeometry,0,71,2;MeshMate@W3D,W3dMeshModeller,7,72,2;MeshMateFace@W3D,W3dMeshModeller,1,73,2;MeshMateGrid@W3D,W3dMeshModeller,8,74,2;MeshMateGridLine@W3D,W3dMeshModeller,1,75,2;MeshMateLine@W3D,W3dMeshModeller,1,76,2\">");
             writer.WriteRaw($"\n<EplPxfDir>");

             writer.WriteRaw($"\n<EplPxfDirEntry MacroFuncType =\"1\" VariantId=\"0\"/>");
             writer.WriteRaw($"\n</EplPxfDir>");

             writer.WriteRaw($"\n<MacroVariant MacroFuncType =\"1\" VariantId=\"0\" ReferencePoint=\"83/190/0\" Version=\"2.8.12959\" PxfVersion=\"1.23\" SchemaVersion=\"1.7.12959\" Source=\"\" SourceProject=\"\" Description=\"\" ConfigurationFlags=\"0\" DocumentType=\"1\" SelectionArea=\"83/198/91/190\" Customgost=\"0\">");
             writer.WriteRaw($"\n<O4 Build =\"12959\" A1=\"4/54\" A3=\"0\" A13=\"0\" A14=\"0\" A47=\"1\" A48=\"1675154210\" A50=\"0\" A59=\"1\" A404=\"17\" A405=\"64\" A406=\"0\" A407=\"0\" A431=\"1\" A699=\"00000000000000000000000000000000\" A700=\"00000000-0000-0000-0000-000000000000\" A1101=\"2\" A1102=\"\" A1103=\"\" A4043=\"0\" A4049=\"0\">");
             writer.WriteRaw($"\n<P49 P11012 =\"2\"/>");
             writer.WriteRaw($"\n<O37 Build =\"12959\" A1=\"37/18381\" A3=\"1\" A13=\"0\" A14=\"0\" A404=\"1\" A405=\"64\" A406=\"0\" A407=\"0\" A682=\"0\" A683=\"144/18382\" A684=\"1\" A685=\"83/190\" A687=\"8\" A688=\"2\" A689=\"1\" A690=\"0\" A691=\"0\" A693=\"0\" A695=\"5\" A699=\"00000000000000000000000000000000\" A702=\"1\" A792=\"0\" A793=\"0\" A794=\"0\" A1261=\"0\" A1262=\"44\" A1263=\"0\" A1631=\"0/-8\" A1632=\"8/0\">");
             writer.WriteRaw($"\n<S109x692 R1906 =\"144/18382\"/>");
             writer.WriteRaw($"\n<S40x1201 A762 =\"83/198\">");
             writer.WriteRaw($"\n<S39x761 A751 =\"1\" A752=\"0\" A753=\"0\" A754=\"1\"/>");
             writer.WriteRaw($"\n</S40x1201>");

             writer.WriteRaw($"\n<P11 P11008_1 =\"\" P23012=\"0\" P23013=\"2\"/>");
             writer.WriteRaw($"\n<S89x5 Build =\"12959\" A3=\"0\" A4=\"1\" R7=\"37/18381\" A13=\"0\" A14=\"0\" A189=\"0\" A404=\"9\" A405=\"64\" A406=\"0\" A407=\"0\" A411=\"308\" A412=\"L\" A413=\"L\" A414=\"L\" A415=\"L\" A416=\"0\" A1651=\"0/-8\" A1652=\"8/0\" A1653=\"0\" A1654=\"0\" A1655=\"0\" A1656=\"0\" A1657=\"0\"/>");
             writer.WriteRaw($"\n</O37>");

             writer.WriteRaw($"\n<O5 Build =\"12959\" A1=\"5/18383\" A3=\"3\" A13=\"0\" A14=\"0\" A61=\"0\" A62=\"1675154242\" A63=\"#COMMENT\" A67=\"\" A68=\"0\" A69=\"0\">");
             writer.WriteRaw($"\n<S165x5 Build =\"1\" R2=\"19/3\" A3=\"0\" A4=\"1\" R7=\"5/18383\" A13=\"0\" A14=\"0\" A189=\"0\" A404=\"33\" A405=\"64\" A406=\"0\" A407=\"0\" A411=\"519\" A412=\"0\" A413=\"40\" A414=\"0.35\" A415=\"-0\" A416=\"0\" A501=\"0/-6\" A503=\"0\" A506=\"22\" A511=\"{sCommentText}\" A2521=\"MEIJIN\" A2522=\"\" A2523=\"\" A2524=\"{sDateOfCreation}\" A2525=\"{sDateOfCreation}\" A2526=\"2\" A2527=\"{sStatus}\" A2528=\"0\" A2529=\"0\" A2531=\"0\" A2532=\"0\" A2533=\"6/0;0/-6\" A2534=\"2\" A2539=\"0\" A2540=\"0\">");
             writer.WriteRaw($"\n<S54x505 A961 =\"L\" A962=\"L\" A963=\"0\" A964=\"L\" A965=\"0\" A966=\"0\" A967=\"0\" A968=\"0\" A969=\"0\" A4000=\"L\" A4001=\"L\" A4013=\"0\"/>");
             writer.WriteRaw($"\n</S165x5>");

             writer.WriteRaw($"\n<S30x5 Build =\"1\" R2=\"19/3\" A3=\"0\" A4=\"2\" R7=\"5/18383\" A13=\"0\" A14=\"0\" A189=\"0\" A404=\"1\" A405=\"64\" A406=\"0\" A407=\"0\" A411=\"519\" A412=\"L\" A413=\"L\" A414=\"L\" A415=\"L\" A416=\"0\" A501=\"3/-3\" A503=\"0\" A506=\"0\" A511=\"{sCommentIcon}\">");
             writer.WriteRaw($"\n<S54x505 A961 =\"2.5\" A962=\"0\" A963=\"0\" A964=\"L\" A965=\"0\" A966=\"5\" A967=\"0\" A968=\"0\" A969=\"0\" A4000=\"L\" A4001=\"L\" A4013=\"0\"/>");
             writer.WriteRaw($"\n</S30x5>");

             writer.WriteRaw($"\n<S30x5 Build =\"1\" R2=\"19/3\" A3=\"0\" A4=\"3\" R7=\"5/18383\" A13=\"0\" A14=\"0\" A189=\"0\" A404=\"33\" A405=\"64\" A406=\"0\" A407=\"0\" A411=\"519\" A412=\"L\" A413=\"L\" A414=\"L\" A415=\"L\" A416=\"0\" A501=\"7/0\" A503=\"0\" A506=\"0\" A511=\"{sCommentText}\">");
             writer.WriteRaw($"\n<S54x505 A961 =\"L\" A962=\"0\" A963=\"278\" A964=\"L\" A965=\"16\" A966=\"1\" A967=\"50\" A968=\"20\" A969=\"64\" A4000=\"L\" A4001=\"L\" A4013=\"0\"/>");
             writer.WriteRaw($"\n</S30x5>");

             writer.WriteRaw($"\n</O5>");

             writer.WriteRaw($"\n<O144 Build =\"12959\" A1=\"144/18382\" A3=\"0\" R9=\"5/18383\" A13=\"0\" A14=\"0\" A404=\"1\" A405=\"64\" A406=\"0\" A407=\"0\">");
             writer.WriteRaw($"\n<S40x1201 A762 =\"84/197\">");
             writer.WriteRaw($"\n<S39x761 A751 =\"1\" A752=\"0\" A753=\"0\" A754=\"1\"/>");
             writer.WriteRaw($"\n</S40x1201>");

             writer.WriteRaw($"\n</O144>");
             writer.WriteRaw($"\n</O4>");
             writer.WriteRaw($"\n</MacroVariant>");
             writer.WriteRaw($"\n</EplanPxfRoot>");*/


            writer.WriteRaw($"\n<EplanPxfRoot Name =\"comment5\" Type=\"WindowMacro\" Version=\"2.8.12959\" PxfVersion=\"1.23\" SchemaVersion=\"1.7.12959\" Source=\"\" SourceProject=\"\" Description=\"\" ConfigurationFlags=\"8193\" NumMainObjects=\"1\" NumProjectSteps=\"1\" NumMDSteps=\"0\" Custompagescaleused=\"true\" StreamSchema=\"EBitmap, ,2,1,2;EPosition3D, ,0,3,2;ERay3D, ,0,4,2;EStreamableVector, ,0,5,2;BaseTextLine@W3D, ,2,22,2;Box3d@W3D, ,0,23,2;DMNCDataSet, ,1,25,2;DMNCDataSetVector, ,1,26,2;Linetype@W3D, ,0,28,2;Color@W3D, ,0,29,2;Material@W3D, ,3,30,2;Mesh3d@W3D, ,8,32,2;Mesh3dX@W3D, ,2,33,2;MeshBox@W3D, ,5,34,2;MeshEdge3d@W3D, ,0,35,2;MeshFace3d@W3D, ,2,36,2;MeshPoint3d@W3D, ,1,37,2;MeshPolygon3d@W3D, ,1,38,2;MeshSimpleTextureTriangle3d@W3D, ,2,39,2;MeshSimpleTriangle3d@W3D, ,1,40,2;MeshText3dX@W3D, ,0,41,2;MeshTriangle3d@W3D, ,2,42,2;MeshTriangleFace3d@W3D, ,0,43,2;MeshTriangleFaceEdge3D@W3D, ,0,44,2;CTexture@W3D,W3dBaseGeometry,0,71,2;MeshMate@W3D,W3dMeshModeller,7,72,2;MeshMateFace@W3D,W3dMeshModeller,1,73,2;MeshMateGrid@W3D,W3dMeshModeller,8,74,2;MeshMateGridLine@W3D,W3dMeshModeller,1,75,2;MeshMateLine@W3D,W3dMeshModeller,1,76,2\">");
            writer.WriteRaw($"\n<EplPxfDir>");

            writer.WriteRaw($"\n<EplPxfDirEntry MacroFuncType =\"1\" VariantId=\"0\"/>");
            writer.WriteRaw($"\n</EplPxfDir>");

            writer.WriteRaw($"\n<MacroVariant MacroFuncType =\"1\" VariantId=\"0\" ReferencePoint=\"0/0/0\" Version=\"2.8.12959\" PxfVersion=\"1.23\" SchemaVersion=\"1.7.12959\" Source=\"\" SourceProject=\"\" Description=\"\" ConfigurationFlags=\"0\" DocumentType=\"1\" SelectionArea=\"83/198/91/190\" Customgost=\"0\">");
            writer.WriteRaw($"\n<O14 Build =\"12959\" A1=\"14/1\" A3=\"0\" A13=\"0\" A14=\"0\" A133=\"ST_Macro@1534018255\" A134=\"2.8.3.12959\" A135=\"1\">");
            writer.WriteRaw($"\n</O14>");

            writer.WriteRaw($"\n<O4 Build =\"12959\" A1=\"4/54\" A3=\"0\" A13=\"0\" A14=\"0\" A47=\"1\" A48=\"1675156374\" A50=\"0\" A59=\"1\" A404=\"17\" A405=\"64\" A406=\"0\" A407=\"0\" A431=\"1\" A699=\"00000000000000000000000000000000\" A700=\"00000000-0000-0000-0000-000000000000\" A1101=\"2\" A1102=\"\" A1103=\"\" A4043=\"0\" A4049=\"0\">");
            writer.WriteRaw($"\n<P49 P11012 =\"2\"/>");
            writer.WriteRaw($"\n<O37 Build =\"12959\" A1=\"37/30791\" A3=\"1\" A13=\"0\" A14=\"0\" A404=\"1\" A405=\"64\" A406=\"0\" A407=\"0\" A682=\"0\" A683=\"165/30793;30/30794;30/30795\" A684=\"1\" A685=\"83/190\" A687=\"8\" A688=\"2\" A689=\"1\" A690=\"0\" A691=\"0\" A693=\"0\" A695=\"5\" A699=\"00000000000000000000000000000000\" A702=\"1\" A792=\"0\" A793=\"0\" A794=\"0\" A1261=\"0\" A1262=\"44\" A1263=\"0\" A1631=\"0/-8\" A1632=\"8/0\">");
            writer.WriteRaw($"\n<S109x692 R1906 =\"165/30793\"/>");
            writer.WriteRaw($"\n<S109x692 R1906 =\"30/30794\"/>");
            writer.WriteRaw($"\n<S109x692 R1906 =\"30/30795\"/>");
            writer.WriteRaw($"\n<S40x1201 A762 =\"0/0\">");
            writer.WriteRaw($"\n<S39x761 A751 =\"1\" A752=\"0\" A753=\"0\" A754=\"1\"/>");
            writer.WriteRaw($"\n</S40x1201>");

            writer.WriteRaw($"\n<P11 P23012=\"0\" P23013=\"2\"/>");
            writer.WriteRaw($"\n<S89x5 Build =\"12959\" A3=\"0\" A4=\"1\" R7=\"37/30791\" A13=\"0\" A14=\"0\" A189=\"0\" A404=\"9\" A405=\"64\" A406=\"0\" A407=\"0\" A411=\"308\" A412=\"L\" A413=\"L\" A414=\"L\" A415=\"L\" A416=\"0\" A1651=\"0/-6\" A1652=\"6/0\" A1653=\"0\" A1654=\"0\" A1655=\"0\" A1656=\"0\" A1657=\"0\"/>");
            writer.WriteRaw($"\n</O37>");

            writer.WriteRaw($"\n<O26 Build =\"12959\" A1=\"26/30792\" A3=\"0\" A13=\"0\" A14=\"0\" A404=\"1\" A405=\"64\" A406=\"0\" A407=\"0\" A431=\"1\">");
            writer.WriteRaw($"\n<O165 Build =\"1\" A1=\"165/30793\" A3=\"0\" A13=\"0\" A14=\"0\" A404=\"33\" A405=\"64\" A406=\"0\" A407=\"0\" A411=\"519\" A412=\"0\" A413=\"40\" A414=\"0.35\" A415=\"-0\" A416=\"0\" A501=\"0/0\" A503=\"0\" A506=\"22\" A511=\"{sCommentText}\" A2521=\"{sAuthor}\" A2522=\"\" A2523=\"\" A2524=\"{sDateOfCreation}\" A2525=\"{sDateOfCreation}\" A2526=\"2\" A2527=\"{sStatus}\" A2528=\"0\" A2529=\"0\" A2531=\"0\" A2532=\"0\" A2533=\"0/0;6/6\" A2534=\"2\" A2539=\"0\" A2540=\"0\">");
            writer.WriteRaw($"\n<S54x505 A961 =\"L\" A962=\"L\" A963=\"0\" A964=\"L\" A965=\"0\" A966=\"0\" A967=\"0\" A968=\"0\" A969=\"0\" A4000=\"L\" A4001=\"L\" A4013=\"0\"/>");
            writer.WriteRaw($"\n</O165>");

            writer.WriteRaw($"\n<O30 Build =\"1\" A1=\"30/30794\" A3=\"0\" A13=\"0\" A14=\"0\" A404=\"1\" A405=\"64\" A406=\"0\" A407=\"0\" A411=\"519\" A412=\"L\" A413=\"L\" A414=\"L\" A415=\"L\" A416=\"0\" A501=\"3/3\" A503=\"0\" A506=\"0\" A511=\"{sCommentIcon}\">");
            writer.WriteRaw($"\n<S54x505 A961 =\"2.5\" A962=\"0\" A963=\"0\" A964=\"L\" A965=\"0\" A966=\"5\" A967=\"0\" A968=\"0\" A969=\"0\" A4000=\"L\" A4001=\"L\" A4013=\"0\"/>");
            writer.WriteRaw($"\n</O30>");

            writer.WriteRaw($"\n<O30 Build =\"1\" A1=\"30/30795\" A3=\"0\" A13=\"0\" A14=\"0\" A404=\"33\" A405=\"64\" A406=\"0\" A407=\"0\" A411=\"519\" A412=\"L\" A413=\"L\" A414=\"L\" A415=\"L\" A416=\"0\" A501=\"7/6\" A503=\"0\" A506=\"0\" A511=\"{sCommentText}\">");
            writer.WriteRaw($"\n<S54x505 A961 =\"1.3\" A962=\"0\" A963=\"278\" A964=\"L\" A965=\"16\" A966=\"1\" A967=\"50\" A968=\"20\" A969=\"64\" A4000=\"L\" A4001=\"L\" A4013=\"0\"/>");
            writer.WriteRaw($"\n</O30>");

            writer.WriteRaw($"\n</O26>");

            writer.WriteRaw($"\n</O4>");

            writer.WriteRaw($"\n</MacroVariant>");
            writer.WriteRaw($"\n</EplanPxfRoot>");



            //O165 Комментарий
            //O30 Текст
            //O37 Рамка макроса
            //O26 Группа
            //O76 Слои

            //A501 Позиция X/Y
            //A404 Активировать блок выравнивания FALSE-1 - TRUE-33
            //A411 Слой
            //A412 Тип линии
            //A413 Цвет
            //A414 ширина линии
            //A762 = "0/6" позиция

            //A961 Высота текста
            //A966 Выравнивание текста 5 = по центру 
            //A967 Блок выравнивания - Ширина
            //A968 Блок выравнивания - Высота

            //A1651 = "0/-5" Height
            //A1652 = "5/0" Width
            //A1656 = "1" заливка

            //S89x5 прямоугольник рамки макроса рамка
            //S40x1201 применяется для смещения рамки макроса A762="0/6"

            //A2521 Автор
            //A2524 Дата создания


            // Write the XML to file and close the writer.
            writer.Flush();
            writer.Close();

            //Вставить макрос

            CommandLineInterpreter oCli = new CommandLineInterpreter();
            ActionCallingContext oAcc = new ActionCallingContext();
            oAcc.AddParameter("Name", "XMIaInsertMacro");
            oAcc.AddParameter("filename", sTempFile);
            oAcc.AddParameter("variant", "0");
            oCli.Execute("XGedStartInteractionAction", oAcc);

            //   Close();
            return;
        }


    }
}
