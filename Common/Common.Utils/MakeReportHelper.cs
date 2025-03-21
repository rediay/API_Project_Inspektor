using Common.DTO;
using Common.DTO.RestrictiveLists;
using Microsoft.AspNetCore.Http;
using Microsoft.Reporting.NETCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ThirdParty.Json.LitJson;
using DataTable = System.Data.DataTable;
using LocalReport = Microsoft.Reporting.NETCore.LocalReport;
using ReportParameter = Microsoft.Reporting.NETCore.ReportParameter;
using ReportDataSource = Microsoft.Reporting.NETCore.ReportDataSource;
using Warning = Microsoft.Reporting.NETCore.Warning;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.PTG;
using Org.BouncyCastle.Utilities;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Common.Entities.SPsData.AditionalServices.Procuraduria;
using System.Web.Helpers;
using DocumentFormat.OpenXml.Drawing.Charts;
using Common.DTO.Queries;
using Org.BouncyCastle.Utilities.IO;
using Microsoft.ReportingServices.Interfaces;

namespace Common.Utils
{
    public class MakeReportHelper
    {
        public Object[] makeReport(IndividualQueryResponseDTO dto, HttpResponse response, string contentRootPath)
        {
            using var ReportViewerReportConsolidateNew = new LocalReport();
            ReportViewerReportConsolidateNew.DataSources.Clear();
            ReportViewerReportConsolidateNew.EnableExternalImages = true;
            ReportViewerReportConsolidateNew.EnableHyperlinks = true;
            ReportParameter[] parameters = new ReportParameter[28];
            parameters[0] = new ReportParameter("name_consultation", dto.name.ToString());
            parameters[1] = new ReportParameter("identification_consultation", dto.document.ToString());
            parameters[2] = new ReportParameter("no_consultation", dto.query.IdQueryCompany.ToString());

            String name_user_consultation1 = dto.user.Name != null ? dto.user.Name : dto.user.LastName;
            parameters[3] = new ReportParameter("name_user_consultation", name_user_consultation1.ToString());
            parameters[4] = new ReportParameter("user_consultant", dto.user.Login.ToString());
            parameters[5] = new ReportParameter("date_report", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            parameters[6] = new ReportParameter("date_consultation", dto.query.CreatedAt.ToString());
            String thirdPartyType1 = "";
            if (dto.thirdPartyType != null) 
            {
                thirdPartyType1 = dto.thirdPartyType.Name.ToString();
            }
            parameters[7] = new ReportParameter("type_third", thirdPartyType1.ToString());

            string str_list_consulted = string.Empty;
            int no_list_consulted = dto.listsearch.Count;
            foreach (var item in dto.listsearch)
            {
                str_list_consulted += str_list_consulted.Equals("") ? item.ListType.Name.TrimEnd(' ').TrimStart(' ') : ", " + item.ListType.Name.TrimEnd(' ').TrimStart(' ');
            }
            parameters[8] = new ReportParameter("no_lists_consulted", no_list_consulted.ToString());
            parameters[9] = new ReportParameter("ListsConsulted", str_list_consulted.ToString());
            string img = "";
            if (dto.image != null)
            {
                try
                {
                    img = settingToBase64(dto.image);
                }
                catch (Exception ex) { }
            }
            parameters[10] = new ReportParameter("image", img);
            String rama_judicial_jepms_message = "";
            if (dto.ramaJudicialJEMPS != null)
            {
                if (dto.ramaJudicialJEMPS.ErrorMessage != null)
                {
                    rama_judicial_jepms_message = dto.ramaJudicialJEMPS.ErrorMessage != null ? dto.ramaJudicialJEMPS.ErrorMessage.ToString() : "";
                }
            }
            parameters[11] = new ReportParameter("message_branch_judicial_jepms", rama_judicial_jepms_message.ToString());

            String rama_judicial_message = "";
            if (dto.ramaJudicial != null)
            {
                if (dto.ramaJudicial.ErrorMessage != null)
                {
                    rama_judicial_message = dto.ramaJudicial.ErrorMessage != null ? dto.ramaJudicial.ErrorMessage.ToString() : "";
                }
            }
            parameters[12] = new ReportParameter("message_branch_judicial", rama_judicial_message.ToString());
            
            String procuraduria_message = "";
            if (dto.procuraduria != null)
            {
                if (dto.procuraduria.ErrorMessage != null)
                {
                    procuraduria_message = dto.procuraduria.ErrorMessage != null ? dto.procuraduria.ErrorMessage.ToString() : "";
                }
            }
            parameters[13] = new ReportParameter("message_attorney", procuraduria_message.ToString());

            String military_message = "";
            if (dto.military != null)
            {
                if (dto.military.ErrorMessage != null)
                {
                    military_message = dto.military.ErrorMessage.ToString();
                }
                else if (dto.military.Data != null)
                {
                    military_message = dto.military.Data.Error != null ? dto.military.Data.Error.ToString() : "";
                }
            }
            parameters[14] = new ReportParameter("message_military", military_message.ToString());

            String super_societies_message = "";
            if (dto.superSocieties != null)
            {
                if (dto.superSocieties.ErrorMessage != null)
                {
                    super_societies_message = dto.superSocieties.ErrorMessage != null ? dto.superSocieties.ErrorMessage.ToString() : "";
                }
            }
            parameters[15] = new ReportParameter("message_super_societies", super_societies_message.ToString());

            String rues_message = "";
            if (dto.rues != null)
            {
                if (dto.rues.ErrorMessage != null)
                {
                    rues_message = dto.rues.ErrorMessage != null ? dto.rues.ErrorMessage.ToString() : "";
                }
            }
            parameters[16] = new ReportParameter("message_rues", rues_message.ToString());

            String simit_message = "";
            if (dto.simit != null)
            {
                if (dto.simit.ErrorMessage != null)
                {
                    simit_message = dto.simit.ErrorMessage != null ? dto.simit.ErrorMessage.ToString() : "";
                }
            }
            parameters[17] = new ReportParameter("message_simit", simit_message.ToString());

            string PPT_message = "";
            if (dto.Ppt != null)
            {
                if (dto.Ppt.ErrorMessage != null)
                {
                    PPT_message = dto.Ppt.ErrorMessage != null ? dto.Ppt.ErrorMessage.ToString() : "";
                }
            }
            parameters[18] = new ReportParameter("message_PPT", PPT_message.ToString());

            string mensaje_defRegistraduria = "";
            if (dto.RegistryDeaths != null)
            {
                if (dto.RegistryDeaths.ErrorMessage != null)
                {
                    mensaje_defRegistraduria = dto.RegistryDeaths.ErrorMessage.ToString();
                }
                else if (dto.RegistryDeaths.Data != null)
                {
                    mensaje_defRegistraduria = dto.RegistryDeaths.Data.vigencia != null ? dto.RegistryDeaths.Data.vigencia.ToString() : "";
                }
            }
            parameters[19] = new ReportParameter("message_defRegistraduria", mensaje_defRegistraduria.ToString());

            string EPS_message = "";
            if (dto.eps != null)
            {
                if (dto.eps.ErrorMessage != null)
                {
                    EPS_message = dto.eps.ErrorMessage != null ? dto.eps.ErrorMessage.ToString() : "";
                }
            }
            parameters[20] = new ReportParameter("message_EPS", EPS_message.ToString());

            String criminal_records_ecuador_message = "";
            if (dto.criminalRecordEcuador != null)
            {
                if (dto.criminalRecordEcuador.ErrorMessage != null)
                {
                    criminal_records_ecuador_message = dto.criminalRecordEcuador.ErrorMessage.ToString();
                }
                else if (dto.criminalRecordEcuador.Data != null)
                {
                    criminal_records_ecuador_message = dto.criminalRecordEcuador.Data.error != null ? dto.criminalRecordEcuador.Data.error.ToString() : "";
                }
            }
            parameters[21] = new ReportParameter("message_criminal_records_ecuador", criminal_records_ecuador_message.ToString());

            String judicial_information_mensaage = "";
            if (dto.infoJudicialEcuador != null)
            {
                if (dto.infoJudicialEcuador.ErrorMessage != null)
                {
                    judicial_information_mensaage = dto.infoJudicialEcuador.ErrorMessage != null ? dto.infoJudicialEcuador.ErrorMessage.ToString() : "";
                }
            }
            parameters[22] = new ReportParameter("message_judicial_information_ecuador", judicial_information_mensaage.ToString());

            String sunat_message = "";
            if (dto.sunat != null)
            {
                if (dto.sunat.ErrorMessage != null)
                {
                    sunat_message = dto.sunat.ErrorMessage != null ? dto.sunat.ErrorMessage.ToString() : "";
                }
            }
            parameters[23] = new ReportParameter("message_sunat", sunat_message.ToString());

            String police_message = "";
            if (dto.police != null)
            {
                if (dto.police.ErrorMessage != null)
                {
                    police_message = dto.police.ErrorMessage.ToString();
                }
                else if (dto.police.Data != null)
                {
                    police_message = dto.police.Data.message.text_result != null ? dto.police.Data.message.text_result.ToString() : "";
                }
            }
            parameters[24] = new ReportParameter("message_police", police_message.ToString());

            String bme_text_message = "";
            String bme_Incumplimiento_acuerdo_message = "";
            String bme_message = "";
            if (dto.bme != null)
            {
                if (dto.bme.Data != null)
                {
                    bme_text_message = dto.bme.Data.bme_text.ToString();
                    bme_Incumplimiento_acuerdo_message = dto.bme.Data.incumplimiento_acuerdos.ToString();
                }

                else if (dto.bme.ErrorMessage != null)
                {
                    bme_message = dto.bme.ErrorMessage.ToString();
                }
            }
            
            parameters[25] = new ReportParameter("message_bme_text", bme_text_message.ToString());
            parameters[26] = new ReportParameter("message_bme_Incumplimiento_acuerdo", bme_Incumplimiento_acuerdo_message.ToString());
            parameters[27] = new ReportParameter("message_bme", bme_message.ToString());

            List<ListDTO> restrictiveLists = new List<ListDTO>();
            List<ListDTO> laftPenalLists = new List<ListDTO>();
            List<ListDTO> laftAdminLists = new List<ListDTO>();
            List<ListDTO> sanctionsAffectationLists = new List<ListDTO>();
            List<ListDTO> financialAffectationLists = new List<ListDTO>();
            List<ListDTO> pepsLists = new List<ListDTO>();
            List<ListDTO> informativeLists = new List<ListDTO>();
            foreach (var item in dto.lists)
            {
                switch (item.ListGroupId)
                {
                    case 1:
                        restrictiveLists.Add(item);
                        break;
                    case 2:
                        laftPenalLists.Add(item);
                        break;
                    case 3:
                        laftAdminLists.Add(item);
                        break;
                    case 4:
                        sanctionsAffectationLists.Add(item);
                        break;
                    case 5:
                        financialAffectationLists.Add(item);
                        break;
                    case 6:
                        pepsLists.Add(item);
                        break;
                    case 7:
                        informativeLists.Add(item);
                        break;
                }
            }

            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "restrictiveLists",
                Value = restrictiveLists
            });
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "laftPenalLists",
                Value = laftPenalLists
            });
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "laftAdminLists",
                Value = laftAdminLists
            });
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "sanctionsAffectationLists",
                Value = sanctionsAffectationLists
            });
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "financialAffectationLists",
                Value = financialAffectationLists
            });
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "pepsLists",
                Value = pepsLists
            });
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "informativeLists",
                Value = informativeLists
            });
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "ownLists",
                Value = dto.ownLists
            });

            List<ListDTO> lists_ofac = new List<ListDTO>();
            List<ListDTO> lists_security_onu = new List<ListDTO>();
            List<ListDTO> lists_assets_financial = new List<ListDTO>();
            List<ListDTO> lists_terrorist_police_judicial = new List<ListDTO>();
            List<ListDTO> lists_terrorist_organization = new List<ListDTO>();
            List<ListDTO> lists_eliminated_terrorist_eu = new List<ListDTO>();

            foreach (var item in restrictiveLists)
            {
                switch (item.ListTypeId)
                {
                    case 4:
                        lists_ofac.Add(item);
                        break;
                    case 8:
                        lists_security_onu.Add(item);
                        break;
                    case 158:
                        lists_assets_financial.Add(item);
                        break;
                    case 159:
                        lists_terrorist_police_judicial.Add(item);
                        break;
                    case 160:
                        lists_terrorist_organization.Add(item);
                        break;
                    case 161:
                        lists_eliminated_terrorist_eu.Add(item);
                        break;
                }
            }


            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "lists_ofac",
                Value = lists_ofac
            });


            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "lists_security_onu",
                Value = lists_security_onu
            });


            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "lists_terrorist_assets_financial",
                Value = lists_assets_financial
            });


            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "lists_terrorist_police_judicial",
                Value = lists_terrorist_police_judicial
            });


            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "lists_terrorist_organization",
                Value = lists_terrorist_organization
            });


            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "lists_eliminated_terrorist_eu",
                Value = lists_eliminated_terrorist_eu
            });

            var matrix_risk = new DataTable();
            var rowrisk = matrix_risk.NewRow();
            for (int y = 1; y <= 4; y++)
            {
                for (int x = 1; x <= 5; x++)
                {
                    matrix_risk.Columns.Add(new DataColumn("p" + y + x));
                    rowrisk["p" + y + x] = 0;
                }
            }

            if (dto.lists != null && !dto.lists.Equals("[]"))
            {
                foreach (var item in dto.lists)
                {
                    int order = Int32.Parse(item.Order.ToString().Trim());
                    int priority = Int32.Parse(item.PriorityResult.ToString().Trim());
                    if (order <= 5 && priority <= 4)
                    {
                        int value = Int32.Parse(rowrisk["p" + priority + order].ToString().Trim());
                        value += 1;
                        rowrisk["p" + priority + order] = value;
                    }
                }
            }
            matrix_risk.Rows.Add(rowrisk);

            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "matrix_risk",
                Value = matrix_risk
            });

            var list_branch_judicial_jepms = new DataTable();
            list_branch_judicial_jepms.Columns.Add(new DataColumn("NameResult"));
            list_branch_judicial_jepms.Columns.Add(new DataColumn("IdentificationNumberResult"));
            list_branch_judicial_jepms.Columns.Add(new DataColumn("CityName"));
            list_branch_judicial_jepms.Columns.Add(new DataColumn("link"));

            if (dto.ramaJudicialJEMPS != null && rama_judicial_jepms_message.Equals(""))
            {
                foreach (var item in dto.ramaJudicialJEMPS.Data)
                {
                    DataRow rowBranchJudicial = list_branch_judicial_jepms.NewRow();
                    rowBranchJudicial["NameResult"] = item.NameResult != null ? item.NameResult.ToString() : "";
                    rowBranchJudicial["IdentificationNumberResult"] = item.IdentificationNumberResult != null ? item.IdentificationNumberResult.ToString() : "";
                    rowBranchJudicial["CityName"] = item.CityName != null ? item.CityName.ToString() : "";
                    rowBranchJudicial["Link"] = item.Link != null ? item.Link.ToString() : "";
                    list_branch_judicial_jepms.Rows.Add(rowBranchJudicial);
                }
            }
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "list_branch_judicial_jepms",
                Value = list_branch_judicial_jepms
            });

            var table_list_branch_judicial_administrative = reportCompleteGetRowTablesRama();
            var table_list_branch_judicial_civil = reportCompleteGetRowTablesRama();
            var table_list_branch_judicial_labor = reportCompleteGetRowTablesRama();
            var table_list_branch_judicial_family = reportCompleteGetRowTablesRama();
            var table_list_branch_judicial_JEPMS = reportCompleteGetRowTablesRama();
            var table_list_branch_judicial_other = reportCompleteGetRowTablesRama();

            if (dto.ramaJudicial != null && rama_judicial_message.Equals(""))
            {
                foreach (var item in dto.ramaJudicial.Data)
                {
                    string dispatch = item.despacho.ToString().ToLower();
                    DataRow rowBranchJudicial;
                    if (dispatch.Contains("administrativo"))
                    {
                        rowBranchJudicial = table_list_branch_judicial_administrative.NewRow();
                    }
                    else if (dispatch.Contains("civil"))
                    {
                        rowBranchJudicial = table_list_branch_judicial_civil.NewRow();
                    }
                    else if (dispatch.Contains("laboral"))
                    {
                        rowBranchJudicial = table_list_branch_judicial_labor.NewRow();
                    }
                    else if (dispatch.Contains("familia"))
                    {
                        rowBranchJudicial = table_list_branch_judicial_family.NewRow();
                    }
                    else if (dispatch.Contains("penal") || dispatch.Contains("con función de control de garantías") || dispatch.Contains("con función de conocimiento"))
                    {
                        rowBranchJudicial = table_list_branch_judicial_JEPMS.NewRow();
                    }
                    else
                    {
                        rowBranchJudicial = table_list_branch_judicial_other.NewRow();
                    }

                    rowBranchJudicial["idProcess"] = item.idProceso.ToString();
                    rowBranchJudicial["keyProcess"] = item.llaveProceso.ToString();
                    rowBranchJudicial["dateProcess"] = item.fechaProceso?.ToString();
                    rowBranchJudicial["dateLastPerformance"] = item.fechaUltimaActuacion?.ToString();
                    rowBranchJudicial["dispatch"] = item.despacho?.ToString();
                    rowBranchJudicial["department"] = item.departamento?.ToString();

                    if (item.sujetosProcesales != null && item.sujetosProcesales.ToString().Length > 300)
                        item.sujetosProcesales = item.sujetosProcesales.ToString().Substring(0, 300) + "(Ver mas con el numero de radicado)";
                    rowBranchJudicial["subjectsProcedural"] = item.sujetosProcesales?.ToString();

                    if (dispatch.Contains("administrativo"))
                    {
                        table_list_branch_judicial_administrative.Rows.Add(rowBranchJudicial);
                    }
                    else if (dispatch.Contains("civil"))
                    {

                        table_list_branch_judicial_civil.Rows.Add(rowBranchJudicial);
                    }
                    else if (dispatch.Contains("laboral"))
                    {

                        table_list_branch_judicial_labor.Rows.Add(rowBranchJudicial);
                    }
                    else if (dispatch.Contains("familia"))
                    {

                        table_list_branch_judicial_family.Rows.Add(rowBranchJudicial);
                    }
                    else if (dispatch.Contains("penal") || dispatch.Contains("con función de control de garantías") || dispatch.Contains("con función de conocimiento"))
                    {

                        table_list_branch_judicial_JEPMS.Rows.Add(rowBranchJudicial);
                    }
                    else
                    {
                        table_list_branch_judicial_other.Rows.Add(rowBranchJudicial);
                    }

                }
            }
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "list_branch_judicial_administrative",
                Value = table_list_branch_judicial_administrative
            });
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "list_branch_judicial_civil",
                Value = table_list_branch_judicial_civil
            });
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "list_branch_judicial_labor",
                Value = table_list_branch_judicial_labor
            });
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "list_branch_judicial_family",
                Value = table_list_branch_judicial_family
            });
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "list_branch_judicial_JEPMS",
                Value = table_list_branch_judicial_JEPMS
            });
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "list_branch_judicial_other",
                Value = table_list_branch_judicial_other
            });

            var table_sub_procuraduria = new DataTable();
            table_sub_procuraduria.Columns.Add(new DataColumn("num_siri"));
            table_sub_procuraduria.Columns.Add(new DataColumn("data"));

            if (dto.procuraduria != null && procuraduria_message.Equals(""))
            {

                var TempObjectProcuraduria = dto.procuraduria.Data;
                String html_contaduria = TempObjectProcuraduria.html_response != null ? TempObjectProcuraduria.html_response.ToString() : "";
                if (!html_contaduria.Equals("") && html_contaduria.Contains("Datos del ciudadano") && !html_contaduria.Contains("El ciudadano no presenta antecedentes"))
                {
                    try
                    {
                        html_contaduria = StringBetween(html_contaduria, "Datos del ciudadano", ".  &lt");
                        html_contaduria = html_contaduria.Replace("&lt;", "").Replace("/span&gt;", "").Replace("span&gt;", " ").Replace("/h2&gt;div class=\\&quot;datosConsultado\\&quot;&gt;", "").Trim();
                        html_contaduria += "<br>" + procuraduria_message;
                        parameters[13] = new ReportParameter("message_attorney", html_contaduria);
                    }
                    catch (Exception ex) { }
                }
                var TempjsonObjectProcuraduriaString = TempObjectProcuraduria.data.ToString();
                if (TempObjectProcuraduria.data != null && !TempjsonObjectProcuraduriaString.Equals("") && !TempjsonObjectProcuraduriaString.Equals("[]"))
                {
                    foreach (var jsonObjectProcuraduria in TempObjectProcuraduria.data)
                    {
                        var row = table_sub_procuraduria.NewRow();
                        var itemPocuraduria = TempObjectProcuraduria.data[0];
                        row["num_siri"] = itemPocuraduria.num_siri;
                        row["data"] = JsonConvert.SerializeObject(TempObjectProcuraduria.data); //TempObjectProcuraduria.data.ToString();
                        table_sub_procuraduria.Rows.Add(row);
                    }
                }
            }

            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "sub_procuraduria",
                Value = table_sub_procuraduria
            });

            var table_list_military = new DataTable();
            table_list_military.Columns.Add(new DataColumn("ReservationType"));
            table_list_military.Columns.Add(new DataColumn("name"));
            table_list_military.Columns.Add(new DataColumn("place"));
            table_list_military.Columns.Add(new DataColumn("address"));

            if (dto.military != null && military_message.Equals(""))
            {
                var rowEjercito = table_list_military.NewRow();
                rowEjercito["ReservationType"] = dto.military.Data.ReservationType;
                rowEjercito["name"] = dto.military.Data.Name;
                rowEjercito["place"] = dto.military.Data.Place;
                rowEjercito["address"] = dto.military.Data.Address;
                table_list_military.Rows.Add(rowEjercito);
            }
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "list_Military",
                Value = table_list_military
            });

            var table_list_super_societies = new DataTable();

            table_list_super_societies.Columns.Add(new DataColumn("ActivityCIIU"));
            table_list_super_societies.Columns.Add(new DataColumn("VersionCIIU"));
            table_list_super_societies.Columns.Add(new DataColumn("DescriptionCIIU"));
            table_list_super_societies.Columns.Add(new DataColumn("AddressJudicial"));
            table_list_super_societies.Columns.Add(new DataColumn("CityJudicial"));
            table_list_super_societies.Columns.Add(new DataColumn("DepartmentJudicial"));
            table_list_super_societies.Columns.Add(new DataColumn("AddresHome"));
            table_list_super_societies.Columns.Add(new DataColumn("Cityhome"));
            table_list_super_societies.Columns.Add(new DataColumn("DepartmentDomicilio"));
            table_list_super_societies.Columns.Add(new DataColumn("Email"));
            table_list_super_societies.Columns.Add(new DataColumn("Nit"));
            table_list_super_societies.Columns.Add(new DataColumn("Businessname"));
            table_list_super_societies.Columns.Add(new DataColumn("Proceedings"));
            table_list_super_societies.Columns.Add(new DataColumn("Initials"));
            table_list_super_societies.Columns.Add(new DataColumn("Socialobject"));
            table_list_super_societies.Columns.Add(new DataColumn("TypeCompany"));
            table_list_super_societies.Columns.Add(new DataColumn("State"));
            table_list_super_societies.Columns.Add(new DataColumn("DateState"));
            table_list_super_societies.Columns.Add(new DataColumn("StageSituation"));
            table_list_super_societies.Columns.Add(new DataColumn("DateSituation"));
            table_list_super_societies.Columns.Add(new DataColumn("DateStage"));
            table_list_super_societies.Columns.Add(new DataColumn("Causal"));
            table_list_super_societies.Columns.Add(new DataColumn("Counter"));
            table_list_super_societies.Columns.Add(new DataColumn("ReviewerFiscal"));
            table_list_super_societies.Columns.Add(new DataColumn("RepresentativeLegal"));
            table_list_super_societies.Columns.Add(new DataColumn("RepresentativeLegalFirstSubstitute"));

            var table_list_board_main = new DataTable();
            table_list_board_main.Columns.Add(new DataColumn("Name"));
            var table_list_board_substitute = new DataTable();
            table_list_board_substitute.Columns.Add(new DataColumn("Name"));

            if (dto.superSocieties != null && super_societies_message.Equals(""))
            {
                var rowSuperSocieties = table_list_super_societies.NewRow();
                rowSuperSocieties["ActivityCIIU"] = dto.superSocieties.data.ActividadCIIU;
                rowSuperSocieties["VersionCIIU"] = dto.superSocieties.data.VersionCIIU;
                rowSuperSocieties["DescriptionCIIU"] = dto.superSocieties.data.DescripcionCIIU;
                rowSuperSocieties["AddressJudicial"] = dto.superSocieties.data.DireccionJudicial;
                rowSuperSocieties["CityJudicial"] = dto.superSocieties.data.CiudadJudicial;
                rowSuperSocieties["DepartmentJudicial"] = dto.superSocieties.data.DepartamentoJudicial;
                rowSuperSocieties["Cityhome"] = dto.superSocieties.data.CiudadDomicilio;
                rowSuperSocieties["DepartmentHome"] = dto.superSocieties.data.DepartamentoJudicial;
                rowSuperSocieties["AddresHome"] = dto.superSocieties.data.DireccionDomicilio;
                rowSuperSocieties["Email"] = dto.superSocieties.data.CorreoElectronico;
                rowSuperSocieties["Nit"] = dto.superSocieties.data.Nit;
                rowSuperSocieties["Businessname"] = dto.superSocieties.data.RazonSocial;
                rowSuperSocieties["Proceedings"] = dto.superSocieties.data.Expediente;
                rowSuperSocieties["Initials"] = dto.superSocieties.data.Sigla;
                rowSuperSocieties["Socialobject"] = dto.superSocieties.data.ObjetoSocial;
                rowSuperSocieties["TypeCompany"] = dto.superSocieties.data.TipoSociedad;
                rowSuperSocieties["State"] = dto.superSocieties.data.Estado;
                rowSuperSocieties["DateState"] = dto.superSocieties.data.FechaEstado;
                rowSuperSocieties["StageSituation"] = dto.superSocieties.data.EtapaSituacion;
                rowSuperSocieties["DateSituation"] = dto.superSocieties.data.FechaSituacion;
                rowSuperSocieties["DateStage"] = dto.superSocieties.data.FechaEtapa;
                rowSuperSocieties["Causal"] = dto.superSocieties.data.Causal;
                rowSuperSocieties["Counter"] = dto.superSocieties.data.Contador;
                rowSuperSocieties["ReviewerFiscal"] = dto.superSocieties.data.RevisorFiscal;
                rowSuperSocieties["RepresentativeLegal"] = dto.superSocieties.data.RepresentanteLegal;
                rowSuperSocieties["RepresentativeLegalFirstSubstitute"] = dto.superSocieties.data.RepresentanteLegalPrimerSuplente;
                table_list_super_societies.Rows.Add(rowSuperSocieties);

                var jsonBoardDirectorsPrincipal = dto.superSocieties.data.JuntaDirectivaPrincipal;
                foreach (var item in jsonBoardDirectorsPrincipal)
                {
                    var rowListsBoartdMain = table_list_board_main.NewRow();
                    rowListsBoartdMain["Name"] = item;
                    table_list_board_main.Rows.Add(rowListsBoartdMain);
                }
                var jsonBoardDirectorsSubstitute = dto.superSocieties.data.JuntaDirectivaSuplente;
                foreach (var item in jsonBoardDirectorsSubstitute)
                {
                    var rowListsBoardSubstitute = table_list_board_substitute.NewRow();
                    rowListsBoardSubstitute["Name"] = item;
                    table_list_board_substitute.Rows.Add(rowListsBoardSubstitute);
                }

            }
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "list_super_societies",
                Value = table_list_super_societies
            });
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "list_board_main",
                Value = table_list_board_main
            });
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "list_board_substitute",
                Value = table_list_board_substitute
            });

            var table_list_rues = new DataTable();

            if (dto.rues != null && rues_message.Equals(""))
            {
                table_list_rues.Columns.Add(new DataColumn("CompanyReasonOrName"));
                table_list_rues.Columns.Add(new DataColumn("Nit"));
                table_list_rues.Columns.Add(new DataColumn("State"));
                table_list_rues.Columns.Add(new DataColumn("Municipality"));
                table_list_rues.Columns.Add(new DataColumn("Category"));

                foreach (var item in dto.rues.ListData)
                {
                    var rowRues = table_list_rues.NewRow();
                    rowRues["CompanyReasonOrName"] = item.RazonSocialONombre.ToString();
                    rowRues["Nit"] = item.Nit.ToString();
                    rowRues["State"] = item.Estado.ToString();
                    rowRues["Municipality"] = item.municipio.ToString();
                    rowRues["Category"] = item.Categoria;
                    table_list_rues.Rows.Add(rowRues);
                }
            }
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "list_rues",
                Value = table_list_rues
            });

            var table_list_simit_new = new DataTable();
            table_list_simit_new.Columns.Add(new DataColumn("type"));
            table_list_simit_new.Columns.Add(new DataColumn("notification"));
            table_list_simit_new.Columns.Add(new DataColumn("plate"));
            table_list_simit_new.Columns.Add(new DataColumn("secretaryship"));
            table_list_simit_new.Columns.Add(new DataColumn("infringement"));
            table_list_simit_new.Columns.Add(new DataColumn("state"));
            table_list_simit_new.Columns.Add(new DataColumn("amount"));
            table_list_simit_new.Columns.Add(new DataColumn("amountToPaid"));

            if (dto.simit != null && simit_message.Equals(""))
            {
                foreach (var item in dto.simit.Data)
                {
                    var rowsimit = table_list_simit_new.NewRow();
                    rowsimit["type"] = item.type;
                    rowsimit["notification"] = item.notification;
                    rowsimit["plate"] = item.plate;
                    rowsimit["secretaryship"] = item.secretaryship;
                    rowsimit["infringement"] = item.infringement;
                    rowsimit["state"] = item.state;
                    rowsimit["amount"] = item.amount;
                    rowsimit["amountToPaid"] = item.amountToPaid;
                    table_list_simit_new.Rows.Add(rowsimit);
                }
            }
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "list_simit_new",
                Value = table_list_simit_new
            });

            var table_TTP = new DataTable();
            table_TTP.Columns.Add(new DataColumn("Name"));
            table_TTP.Columns.Add(new DataColumn("IdentificationType"));
            table_TTP.Columns.Add(new DataColumn("Identification"));
            table_TTP.Columns.Add(new DataColumn("Email"));
            table_TTP.Columns.Add(new DataColumn("Phone"));
            table_TTP.Columns.Add(new DataColumn("Status"));

            if (dto.Ppt != null && PPT_message.Equals(""))
            {
                if (dto.Ppt.Data.Name != null && dto.Ppt.Data.Identification != null)
                {
                    var row = table_TTP.NewRow();
                    row["Name"] = dto.Ppt.Data.Name.ToString();
                    row["IdentificationType"] = dto.Ppt.Data.IdentificationType.ToString();
                    row["Identification"] = dto.Ppt.Data.Identification.ToString();
                    row["Email"] = dto.Ppt.Data.Correo.ToString();
                    row["Phone"] = dto.Ppt.Data.Telefono.ToString();
                    row["Status"] = dto.Ppt.Data.Status.ToString();
                    table_TTP.Rows.Add(row);
                }

            }
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "PPT",
                Value = table_TTP
            });

            var table_EPS = new DataTable();
            table_EPS.Columns.Add(new DataColumn("State"));
            table_EPS.Columns.Add(new DataColumn("Entity"));
            table_EPS.Columns.Add(new DataColumn("Regime"));
            table_EPS.Columns.Add(new DataColumn("EffectiveDate"));
            table_EPS.Columns.Add(new DataColumn("EndDate"));
            table_EPS.Columns.Add(new DataColumn("AffiliateType"));

            if (dto.eps != null && EPS_message.Equals(""))
            {
                foreach (var eps in dto.eps.ListData)
                {
                    var row = table_EPS.NewRow();
                    row["State"] = eps.State.ToString();
                    row["Entity"] = eps.Entity.ToString();
                    row["Regime"] = eps.Regime.ToString();
                    row["EffectiveDate"] = eps.EffectiveDate.ToString();
                    row["EndDate"] = eps.EndDate.ToString();
                    row["AffiliateType"] = eps.AffiliateType.ToString();
                    table_EPS.Rows.Add(row);
                }

            }
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "EPS",
                Value = table_EPS
            });

            var table_criminal_records_ecuador = new DataTable();
            table_criminal_records_ecuador.Columns.Add(new DataColumn("criminalRecord"));
            table_criminal_records_ecuador.Columns.Add(new DataColumn("documentType"));
            table_criminal_records_ecuador.Columns.Add(new DataColumn("document"));
            table_criminal_records_ecuador.Columns.Add(new DataColumn("name"));

            if (dto.criminalRecordEcuador != null && criminal_records_ecuador_message.Equals(""))
            {
                var row = table_criminal_records_ecuador.NewRow();
                row["criminalRecord"] = dto.criminalRecordEcuador.Data.criminalRecord != null ? dto.criminalRecordEcuador.Data.criminalRecord.ToString() : "";
                row["documentType"] = dto.criminalRecordEcuador.Data.documentType != null ? dto.criminalRecordEcuador.Data.documentType.ToString() : "";
                row["document"] = dto.criminalRecordEcuador.Data.document != null ? dto.criminalRecordEcuador.Data.document.ToString() : "";
                row["name"] = dto.criminalRecordEcuador.Data.name != null ? dto.criminalRecordEcuador.Data.name.ToString() : "";

                table_criminal_records_ecuador.Rows.Add(row);

            }
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "criminal_records_ecuador",
                Value = table_criminal_records_ecuador
            });

            var table_judicial_information_ecuador = new DataTable();
            table_judicial_information_ecuador.Columns.Add(new DataColumn("date"));
            table_judicial_information_ecuador.Columns.Add(new DataColumn("nProccess"));
            table_judicial_information_ecuador.Columns.Add(new DataColumn("action"));

            if (dto.infoJudicialEcuador != null && judicial_information_mensaage.Equals(""))
            {
                foreach (var item in dto.infoJudicialEcuador.Data)
                {

                    var row = table_judicial_information_ecuador.NewRow();
                    row["date"] = item.date != null ? item.date.ToString() : "";
                    row["nProccess"] = item.nProccess != null ? item.nProccess.ToString() : "";
                    row["action"] = item.action != null ? item.action.ToString() : "";

                    table_judicial_information_ecuador.Rows.Add(row);

                }
            }
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "judicial_information_ecuador",
                Value = table_judicial_information_ecuador
            });

            var table_sunat = new DataTable();
            table_sunat.Columns.Add(new DataColumn("no_ruc"));
            table_sunat.Columns.Add(new DataColumn("type_cont"));
            table_sunat.Columns.Add(new DataColumn("name_commercial"));
            table_sunat.Columns.Add(new DataColumn("date_inscription"));
            table_sunat.Columns.Add(new DataColumn("date_start_activity"));
            table_sunat.Columns.Add(new DataColumn("state"));
            table_sunat.Columns.Add(new DataColumn("condition"));
            table_sunat.Columns.Add(new DataColumn("domicile"));
            table_sunat.Columns.Add(new DataColumn("emission_system"));
            table_sunat.Columns.Add(new DataColumn("activity_trade_abroad"));
            table_sunat.Columns.Add(new DataColumn("system_accounting"));
            table_sunat.Columns.Add(new DataColumn("activities_economic"));
            table_sunat.Columns.Add(new DataColumn("proof_payment"));
            table_sunat.Columns.Add(new DataColumn("system_emission_electronic"));
            table_sunat.Columns.Add(new DataColumn("emission_electronic_from"));
            table_sunat.Columns.Add(new DataColumn("vouchers_electronic"));
            table_sunat.Columns.Add(new DataColumn("affiliate_ple"));
            table_sunat.Columns.Add(new DataColumn("patterns"));

            if (dto.sunat != null && sunat_message.Equals(""))
            {
                var row = table_sunat.NewRow();
                row["no_ruc"] = dto.sunat.Data.no_ruc != null ? dto.sunat.Data.no_ruc.ToString() : "";
                row["type_cont"] = dto.sunat.Data.tipo_cont != null ? dto.sunat.Data.tipo_cont.ToString() : "";
                row["name_commercial"] = dto.sunat.Data.nombre_comercial != null ? dto.sunat.Data.nombre_comercial.ToString() : "";
                row["date_inscription"] = dto.sunat.Data.fecha_inscripcion != null ? dto.sunat.Data.fecha_inscripcion.ToString() : "";
                row["date_start_activity"] = dto.sunat.Data.fecha_inicio_actividades != null ? dto.sunat.Data.fecha_inicio_actividades.ToString() : "";
                row["state"] = dto.sunat.Data.estado != null ? dto.sunat.Data.estado.ToString() : "";
                row["condition"] = dto.sunat.Data.condicion != null ? dto.sunat.Data.condicion.ToString() : "";
                row["domicile"] = dto.sunat.Data.domicilio != null ? dto.sunat.Data.domicilio.ToString() : "";
                row["emission_system"] = dto.sunat.Data.sistema_emicion != null ? dto.sunat.Data.sistema_emicion.ToString() : "";
                row["activity_trade_abroad"] = dto.sunat.Data.actividad_comercio_exterior != null ? dto.sunat.Data.actividad_comercio_exterior.ToString() : "";
                row["system_accounting"] = dto.sunat.Data.sistema_contabilidad != null ? dto.sunat.Data.sistema_contabilidad.ToString() : "";
                row["activities_economic"] = dto.sunat.Data.actividades_economicas != null ? dto.sunat.Data.actividades_economicas.ToString() : "";
                row["proof_payment"] = dto.sunat.Data.comprobante_pago != null ? dto.sunat.Data.comprobante_pago.ToString() : "";
                row["system_emission_electronic"] = dto.sunat.Data.sistema_emision_electronica != null ? dto.sunat.Data.sistema_emision_electronica.ToString() : "";
                row["emission_electronic_from"] = dto.sunat.Data.emision_electronica_desde != null ? dto.sunat.Data.emision_electronica_desde.ToString() : "";
                row["vouchers_electronic"] = dto.sunat.Data.comprobantes_electronicos != null ? dto.sunat.Data.comprobantes_electronicos.ToString() : "";
                row["affiliate_ple"] = dto.sunat.Data.afiliado_ple != null ? dto.sunat.Data.afiliado_ple.ToString() : "";
                row["patterns"] = dto.sunat.Data.padrones != null ? dto.sunat.Data.padrones.ToString() : "";

                table_sunat.Rows.Add(row);
            }
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "sunat",
                Value = table_sunat
            });

            var table_list_bme = new DataTable();

            if (dto.bme != null && bme_message.Equals(""))
            {
                table_list_bme.Columns.Add(new DataColumn("name_reported"));
                table_list_bme.Columns.Add(new DataColumn("no_obligation"));
                table_list_bme.Columns.Add(new DataColumn("state"));
                table_list_bme.Columns.Add(new DataColumn("date"));

                foreach (var item in dto.bme.Data.bme_data)
                {
                    var rowRues = table_list_bme.NewRow();
                    rowRues["name_reported"] = item.name_reported.ToString();
                    rowRues["no_obligation"] = item.no_obligation.ToString();
                    rowRues["state"] = item.state.ToString();
                    rowRues["date"] = item.date.ToString();
                    table_list_bme.Rows.Add(rowRues);
                }
            }
            ReportViewerReportConsolidateNew.DataSources.Add(new ReportDataSource()
            {
                Name = "list_bme",
                Value = table_list_bme
            });

            ReportViewerReportConsolidateNew.ReportPath = contentRootPath + "\\Reports\\ReportConsolidateNew.rdlc";

            ReportViewerReportConsolidateNew.SetParameters(parameters);
            ReportViewerReportConsolidateNew.Refresh();

            ReportViewerReportConsolidateNew.SubreportProcessing += SubreportProcessingEventHandler;

            return createPdf(ReportViewerReportConsolidateNew, response); 
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            string data = e.Parameters["data"].Values[0];
            //if (!data.Equals("") && !data.Equals("[]"))
            //{

            JsonData jsonObjectProcuraduria = JsonMapper.ToObject(data.ToString());

            var procuraduria = JsonConvert.DeserializeObject<List<ProcuraduriaData>>(data);

            ReportParameter[] parametersProc = new ReportParameter[4];
            string num_siri = procuraduria[0].num_siri.ToString();
            parametersProc[1] = new ReportParameter("num_siri", num_siri);

            var tabla_attorney_sanctions = new DataTable();
            tabla_attorney_sanctions.Columns.Add(new DataColumn("Sanction"));
            tabla_attorney_sanctions.Columns.Add(new DataColumn("Term"));
            tabla_attorney_sanctions.Columns.Add(new DataColumn("Class"));
            tabla_attorney_sanctions.Columns.Add(new DataColumn("Suspended"));
            tabla_attorney_sanctions.Columns.Add(new DataColumn("Suspension_art"));

            var tabla_attorney_crime = new DataTable();
            tabla_attorney_crime.Columns.Add(new DataColumn("Description"));

            var tabla_attorney_instances = new DataTable();
            tabla_attorney_instances.Columns.Add(new DataColumn("Name"));
            tabla_attorney_instances.Columns.Add(new DataColumn("Authority"));
            tabla_attorney_instances.Columns.Add(new DataColumn("Date_province"));
            tabla_attorney_instances.Columns.Add(new DataColumn("Date_effect_legal"));

            var tabla_attorney_events = new DataTable();
            tabla_attorney_events.Columns.Add(new DataColumn("Name_cause"));
            tabla_attorney_events.Columns.Add(new DataColumn("Entity"));
            tabla_attorney_events.Columns.Add(new DataColumn("Type_act"));
            tabla_attorney_events.Columns.Add(new DataColumn("Date_act"));

            var tabla_attorney_disability = new DataTable();
            tabla_attorney_disability.Columns.Add(new DataColumn("Siri"));
            tabla_attorney_disability.Columns.Add(new DataColumn("Module"));
            tabla_attorney_disability.Columns.Add(new DataColumn("Disability_legal"));
            tabla_attorney_disability.Columns.Add(new DataColumn("Date_start"));
            tabla_attorney_disability.Columns.Add(new DataColumn("Date_end"));
            tabla_attorney_disability.Columns.Add(new DataColumn("Suspension_art"));

            var tabla_lost_investiture = new DataTable();
            tabla_lost_investiture.Columns.Add(new DataColumn("Sanction"));
            tabla_lost_investiture.Columns.Add(new DataColumn("Term"));
            tabla_lost_investiture.Columns.Add(new DataColumn("Class_sanction"));
            tabla_lost_investiture.Columns.Add(new DataColumn("Entity"));
            tabla_lost_investiture.Columns.Add(new DataColumn("Position"));



            //if (jsonObjectProcuraduria["antecedentes_penales"] != null)
            //{

            //    foreach (JsonData jsonObject in jsonObjectProcuraduria["antecedentes_penales"])
            //    {
            //        var rowSanctions = tabla_attorney_sanctions.NewRow();

            //        rowSanctions["Sanction"] = jsonObject["sancion"].ToString();
            //        rowSanctions["Term"] = DecodeFromUtf8(jsonObject["termino"].ToString());
            //        rowSanctions["Class"] = DecodeFromUtf8(jsonObject["clase"].ToString());
            //        rowSanctions["Suspended"] = jsonObject["suspendida"].ToString();
            //        if (jsonObject["suspension_art"] != null)
            //            rowSanctions["Suspension_art"] = DecodeFromUtf8(jsonObject["suspension_art"].ToString());
            //        //rowSanciones["Suspension_art"] = jsonObject["suspension_art"] != null ? DecodeFromUtf8(jsonObject["suspension_art"].ToString()) : null;
            //        tabla_attorney_sanctions.Rows.Add(rowSanctions);
            //    }
            //}


            if (procuraduria[0].sanciones != null)
            {

                foreach (var jsonObject in procuraduria[0].sanciones)
                {
                    var rowSanctions = tabla_attorney_sanctions.NewRow();

                    rowSanctions["Sanction"] = jsonObject.sancion.ToString();
                    rowSanctions["Term"] = DecodeFromUtf8(jsonObject.termino.ToString());
                    rowSanctions["Class"] = DecodeFromUtf8(jsonObject.clase.ToString());
                    rowSanctions["Suspended"] = jsonObject.suspendida.ToString();
                    if (jsonObject.Suspension_art != null)
                        rowSanctions["Suspension_art"] = DecodeFromUtf8(jsonObject.Suspension_art.ToString());
                    //rowSanciones["Suspension_art"] = jsonObject["suspension_art"] != null ? DecodeFromUtf8(jsonObject["suspension_art"].ToString()) : null;
                    tabla_attorney_sanctions.Rows.Add(rowSanctions);
                }
            }
            if (procuraduria[0].delitos != null)
            {
                foreach (var jsonObject in procuraduria[0].delitos)
                {
                    var rowCrimes = tabla_attorney_crime.NewRow();

                    rowCrimes["Description"] = DecodeFromUtf8(jsonObject.descripcion.ToString());
                    tabla_attorney_crime.Rows.Add(rowCrimes);
                }
            }
            if (procuraduria[0].instancias != null)
            {
                foreach (var jsonObject in procuraduria[0].instancias)
                {
                    var rowInstances = tabla_attorney_instances.NewRow();

                    rowInstances["Name"] = DecodeFromUtf8(jsonObject.nombre.ToString());
                    rowInstances["Authority"] = DecodeFromUtf8(jsonObject.autoridad.ToString());
                    rowInstances["Date_province"] = DecodeFromUtf8(jsonObject.fecha_provincia.ToString());
                    rowInstances["Date_effect_legal"] = DecodeFromUtf8(jsonObject.fecha_efecto_juridicos.ToString());
                    tabla_attorney_instances.Rows.Add(rowInstances);
                }
            }
            if (procuraduria[0].eventos != null)
            {
                foreach (var jsonObject in procuraduria[0].eventos)
                {
                    var rowEvents = tabla_attorney_events.NewRow();

                    rowEvents["Name_cause"] = jsonObject.nombre_causa.ToString();
                    rowEvents["Entity"] = jsonObject.entidad.ToString();
                    rowEvents["Type_act"] = jsonObject.tipo_acto.ToString();
                    rowEvents["Date_act"] = jsonObject.fecha_acto.ToString();
                    tabla_attorney_events.Rows.Add(rowEvents);
                }
            }
            if (procuraduria[0].inhabilidades != null)
            {
                foreach (var jsonObject in procuraduria[0].inhabilidades)
                {
                    var rowDisability = tabla_attorney_disability.NewRow();

                    rowDisability["Siri"] = DecodeFromUtf8(jsonObject.siri.ToString());
                    rowDisability["Module"] = DecodeFromUtf8(jsonObject.modulo.ToString());
                    rowDisability["Disability_legal"] = jsonObject.inhabilidad_legal.ToString();
                    rowDisability["Date_start"] = DecodeFromUtf8(jsonObject.fecha_inicio.ToString());
                    rowDisability["Date_end"] = DecodeFromUtf8(jsonObject.fecha_fin.ToString());
                    rowDisability["Suspension_art"] = jsonObject.Suspension_art != null ? DecodeFromUtf8(jsonObject.Suspension_art.ToString()) : null;
                    tabla_attorney_disability.Rows.Add(rowDisability);
                }
            }

            if (procuraduria[0].investiduras != null)
            {
                foreach (var jsonObject in procuraduria[0].investiduras)
                {
                    var rowLostI = tabla_lost_investiture.NewRow();

                    rowLostI["Sanction"] = DecodeFromUtf8(jsonObject.Sancion.ToString());
                    rowLostI["Term"] = DecodeFromUtf8(jsonObject.Termino.ToString());
                    rowLostI["Class_sanction"] = DecodeFromUtf8(jsonObject.Clase_sancion.ToString());
                    rowLostI["Entity"] = DecodeFromUtf8(jsonObject.Entidad.ToString());
                    rowLostI["Position"] = DecodeFromUtf8(jsonObject.Cargo.ToString());
                    tabla_lost_investiture.Rows.Add(rowLostI);
                }
            }

            e.DataSources.Add(new ReportDataSource("attorney_sanctions", tabla_attorney_sanctions));
            e.DataSources.Add(new ReportDataSource("attorney_crime", tabla_attorney_crime));
            e.DataSources.Add(new ReportDataSource("attorney_instances", tabla_attorney_instances));
            e.DataSources.Add(new ReportDataSource("attorney_events", tabla_attorney_events));
            e.DataSources.Add(new ReportDataSource("attorney_disability", tabla_attorney_disability));
            e.DataSources.Add(new ReportDataSource("lost_investiture", tabla_lost_investiture));
        }
        protected string DecodeFromUtf8(string utf8String)
        {
            // copy the string as UTF-8 bytes.
            byte[] utf8Bytes = new byte[utf8String.Length];
            for (int i = 0; i < utf8String.Length; ++i)
            {
                //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
                utf8Bytes[i] = (byte)utf8String[i];
            }

            return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
        }
        protected DataTable reportCompleteGetRowTablesRama()
        {
            DataTable tabla_listas_rama_judicial_new = new DataTable();
            tabla_listas_rama_judicial_new.Columns.Add(new DataColumn("idProcess"));
            tabla_listas_rama_judicial_new.Columns.Add(new DataColumn("keyProcess"));
            tabla_listas_rama_judicial_new.Columns.Add(new DataColumn("dateProcess"));
            tabla_listas_rama_judicial_new.Columns.Add(new DataColumn("dateLastPerformance"));
            tabla_listas_rama_judicial_new.Columns.Add(new DataColumn("dispatch"));
            tabla_listas_rama_judicial_new.Columns.Add(new DataColumn("department"));
            tabla_listas_rama_judicial_new.Columns.Add(new DataColumn("subjectsProcedural"));
            return tabla_listas_rama_judicial_new;
        }
        public static string StringBetween(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
        public Object[] makeReportBulk(BulkQueryResponseDTO dto, HttpResponse response, string contentRootPath)
        {
            using var ReportViewerReportConsolidateBulkNew = new LocalReport();
            ReportViewerReportConsolidateBulkNew.DataSources.Clear();
            ReportViewerReportConsolidateBulkNew.EnableExternalImages = true;
            ReportViewerReportConsolidateBulkNew.EnableHyperlinks = true;
            ReportParameter[] parameters = new ReportParameter[9];

            String name_user_consultation1 = dto.query.User.Name != null ? dto.query.User.Name : dto.query.User.LastName;
            parameters[0] = new ReportParameter("name_user_consultation", name_user_consultation1.ToString());
            parameters[1] = new ReportParameter("user_consultant", dto.query.User.Login.ToString());
            parameters[2] = new ReportParameter("date_report", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            parameters[3] = new ReportParameter("date_consultation", dto.query.CreatedAt.ToString());
            String thirdPartyType1 = dto.query.ThirdTypeName != null ? dto.query.ThirdTypeName.ToString() : "";
            parameters[4] = new ReportParameter("type_third", thirdPartyType1.ToString());

            string str_list_consulted = string.Empty;
            int no_list_consulted = dto.listsearch.Count;
            foreach (var item in dto.listsearch)
            {
                str_list_consulted += str_list_consulted.Equals("") ? item.ListType.Name.TrimEnd(' ').TrimStart(' ') : ", " + item.ListType.Name.TrimEnd(' ').TrimStart(' ');
            }
            parameters[5] = new ReportParameter("no_lists_consulted", no_list_consulted.ToString());
            parameters[6] = new ReportParameter("ListsConsulted", str_list_consulted.ToString());
            string img = "";
            if (dto.image != null)
            {
                try
                {
                    img = settingToBase64(dto.image);
                }
                catch (Exception ex) { }
            }
            parameters[7] = new ReportParameter("image", img.ToString());
            parameters[8] = new ReportParameter("no_consultation", dto.query.IdQueryCompany.ToString());

            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "lists_report_bulk",
                Value = dto.QueryDetails
            });

            List<ListDTO> restrictiveLists = new List<ListDTO>();
            List<ListDTO> laftPenalLists = new List<ListDTO>();
            List<ListDTO> laftAdminLists = new List<ListDTO>();
            List<ListDTO> sanctionsAffectationLists = new List<ListDTO>();
            List<ListDTO> financialAffectationLists = new List<ListDTO>();
            List<ListDTO> pepsLists = new List<ListDTO>();
            List<ListDTO> informativeLists = new List<ListDTO>();
            foreach (var item in dto.lists)
            {
                switch (item.ListGroupId)
                {
                    case 1:
                        restrictiveLists.Add(item);
                        break;
                    case 2:
                        laftPenalLists.Add(item);
                        break;
                    case 3:
                        laftAdminLists.Add(item);
                        break;
                    case 4:
                        sanctionsAffectationLists.Add(item);
                        break;
                    case 5:
                        financialAffectationLists.Add(item);
                        break;
                    case 6:
                        pepsLists.Add(item);
                        break;
                    case 7:
                        informativeLists.Add(item);
                        break;
                }
            }

            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "restrictiveLists",
                Value = restrictiveLists
            });
            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "laftPenalLists",
                Value = laftPenalLists
            });
            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "laftAdminLists",
                Value = laftAdminLists
            });
            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "sanctionsAffectationLists",
                Value = sanctionsAffectationLists
            });
            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "financialAffectationLists",
                Value = financialAffectationLists
            });
            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "pepsLists",
                Value = pepsLists
            });
            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "informativeLists",
                Value = informativeLists
            });
            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "ownLists",
                Value = dto.ownLists
            });

            List<ListDTO> lists_ofac = new List<ListDTO>();
            List<ListDTO> lists_security_onu = new List<ListDTO>();
            List<ListDTO> lists_assets_financial = new List<ListDTO>();
            List<ListDTO> lists_terrorist_police_judicial = new List<ListDTO>();
            List<ListDTO> lists_terrorist_organization = new List<ListDTO>();
            List<ListDTO> lists_eliminated_terrorist_eu = new List<ListDTO>();

            foreach (var item in restrictiveLists)
            {
                switch (item.ListTypeId)
                {
                    case 4:
                        lists_ofac.Add(item);
                        break;
                    case 8:
                        lists_security_onu.Add(item);
                        break;
                    case 158:
                        lists_assets_financial.Add(item);
                        break;
                    case 159:
                        lists_terrorist_police_judicial.Add(item);
                        break;
                    case 160:
                        lists_terrorist_organization.Add(item);
                        break;
                    case 161:
                        lists_eliminated_terrorist_eu.Add(item);
                        break;
                }
            }


            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "lists_ofac",
                Value = lists_ofac
            });


            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "lists_security_onu",
                Value = lists_security_onu
            });


            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "lists_terrorist_assets_financial",
                Value = lists_assets_financial
            });


            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "lists_terrorist_police_judicial",
                Value = lists_terrorist_police_judicial
            });


            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "lists_terrorist_organization",
                Value = lists_terrorist_organization
            });


            ReportViewerReportConsolidateBulkNew.DataSources.Add(new ReportDataSource()
            {
                Name = "lists_eliminated_terrorist_eu",
                Value = lists_eliminated_terrorist_eu
            });

            //ReportViewerReportConsolidateBulkNew.ReportPath = contentRootPath + "\\Reports\\ReportConsolidatedBulk.rdlc";
            ReportViewerReportConsolidateBulkNew.ReportPath = contentRootPath + "\\Reports\\ReportConsolidateNewBulk.rdlc";

            ReportViewerReportConsolidateBulkNew.SetParameters(parameters);
            ReportViewerReportConsolidateBulkNew.Refresh();

            return createPdf(ReportViewerReportConsolidateBulkNew, response);
        }
        public PdfDocument makeReportPDFBulkQueryServiceAdditional(BulkQueryServicesAdditionalResponseDTO dto, HttpResponse response, string contentRootPath)
        {
            PdfDocument document = new PdfDocument();

            foreach (var DataThirdType in dto.DataThirdType)
            //for (int i = 0; i < dto.QueryServiceAdditional.TotalConsulting; i++ )
            {
                using var ReportConsolidateNewBulkServicesAdditional = new LocalReport();
                ReportConsolidateNewBulkServicesAdditional.DataSources.Clear();
                ReportConsolidateNewBulkServicesAdditional.EnableExternalImages = true;
                ReportConsolidateNewBulkServicesAdditional.EnableHyperlinks = true;

                ReportParameter[] parameters = new ReportParameter[11];

                string name_consultation = DataThirdType.Name != null ? DataThirdType.Name : "";
                parameters[0] = new ReportParameter("name_consultation", name_consultation);
                string identification_consultation = DataThirdType.Document != null ? DataThirdType.Document : "";
                parameters[1] = new ReportParameter("identification_consultation", identification_consultation);
                parameters[2] = new ReportParameter("no_consultation", dto.QueryServiceAdditional.Id.ToString());

                String name_user_consultation1 = dto.QueryServiceAdditional.User.Name != null ? dto.QueryServiceAdditional.User.Name : dto.QueryServiceAdditional.User.LastName;
                parameters[3] = new ReportParameter("name_user_consultation", name_user_consultation1.ToString());
                parameters[4] = new ReportParameter("user_consultant", dto.QueryServiceAdditional.User.Login.ToString());
                parameters[5] = new ReportParameter("date_report", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                parameters[6] = new ReportParameter("date_consultation", dto.QueryServiceAdditional.CreatedAt.ToString());

                string img = "";
                if (dto.Image != null)
                {
                    try
                    {
                        img = settingToBase64(dto.Image);
                    }
                    catch (Exception ex) { }
                }
                parameters[7] = new ReportParameter("image", img);

                String rama_judicial_jepms_message = "";
                if (DataThirdType.RamaJudicialJEMPS != null)
                {
                    if (DataThirdType.RamaJudicialJEMPS.ErrorMessage != null)
                    {
                        rama_judicial_jepms_message = DataThirdType.RamaJudicialJEMPS.ErrorMessage != null ? DataThirdType.RamaJudicialJEMPS.ErrorMessage.ToString() : "";
                    }
                }
                parameters[8] = new ReportParameter("message_branch_judicial_jepms", rama_judicial_jepms_message.ToString());

                String rama_judicial_message = "";
                if (DataThirdType.RamaJudicial != null)
                {
                    if (DataThirdType.RamaJudicial.ErrorMessage != null)
                    {
                        rama_judicial_message = DataThirdType.RamaJudicial.ErrorMessage != null ? DataThirdType.RamaJudicial.ErrorMessage.ToString() : "";
                    }
                }
                parameters[9] = new ReportParameter("message_branch_judicial", rama_judicial_message.ToString());

                String procuraduria_message = "";
                if (DataThirdType.Procuraduria != null)
                {
                    //if (!string.IsNullOrEmpty(DataThirdType.CheckDigit))
                    //{
                        if (DataThirdType.Procuraduria.ErrorMessage != null)
                        {
                            procuraduria_message = DataThirdType.Procuraduria.ErrorMessage != null ? DataThirdType.Procuraduria.ErrorMessage.ToString() : "";
                        }
                    //}
                    //else
                    //{
                    //    procuraduria_message = "No tiene tipo de identificación";
                    //}
                }
                parameters[10] = new ReportParameter("message_attorney", procuraduria_message.ToString());

                var table_sub_procuraduria = new DataTable();
                table_sub_procuraduria.Columns.Add(new DataColumn("num_siri"));
                table_sub_procuraduria.Columns.Add(new DataColumn("data"));

                if (DataThirdType.Procuraduria != null && procuraduria_message.Equals(""))
                {

                    var TempObjectProcuraduria = DataThirdType.Procuraduria.Data;
                    String html_contaduria = TempObjectProcuraduria.html_response != null ? TempObjectProcuraduria.html_response.ToString() : "";
                    if (!html_contaduria.Equals("") && html_contaduria.Contains("Datos del ciudadano") && !html_contaduria.Contains("El ciudadano no presenta antecedentes"))
                    {
                        try
                        {
                            html_contaduria = StringBetween(html_contaduria, "Datos del ciudadano", ".  &lt");
                            html_contaduria = html_contaduria.Replace("&lt;", "").Replace("/span&gt;", "").Replace("span&gt;", " ").Replace("/h2&gt;div class=\\&quot;datosConsultado\\&quot;&gt;", "").Trim();
                            html_contaduria += "<br>" + procuraduria_message;
                            parameters[10] = new ReportParameter("message_attorney", html_contaduria);
                        }
                        catch (Exception ex) { }
                    }
                    var TempjsonObjectProcuraduriaString = TempObjectProcuraduria.data.ToString();
                    if (TempObjectProcuraduria.data != null && !TempjsonObjectProcuraduriaString.Equals("") && !TempjsonObjectProcuraduriaString.Equals("[]"))
                    {
                        foreach (var jsonObjectProcuraduria in TempObjectProcuraduria.data)
                        {
                            var row = table_sub_procuraduria.NewRow();
                            var itemPocuraduria = TempObjectProcuraduria.data[0];
                            row["num_siri"] = itemPocuraduria.num_siri;
                            row["data"] = JsonConvert.SerializeObject(TempObjectProcuraduria.data); //TempObjectProcuraduria.data.ToString();
                            table_sub_procuraduria.Rows.Add(row);
                        }
                    }
                }

                ReportConsolidateNewBulkServicesAdditional.DataSources.Add(new ReportDataSource()
                {
                    Name = "sub_procuraduria",
                    Value = table_sub_procuraduria
                });

                var table_list_branch_judicial_administrative = reportCompleteGetRowTablesRama();
                var table_list_branch_judicial_civil = reportCompleteGetRowTablesRama();
                var table_list_branch_judicial_labor = reportCompleteGetRowTablesRama();
                var table_list_branch_judicial_family = reportCompleteGetRowTablesRama();
                var table_list_branch_judicial_JEPMS = reportCompleteGetRowTablesRama();
                var table_list_branch_judicial_other = reportCompleteGetRowTablesRama();

                if (DataThirdType.RamaJudicial != null && rama_judicial_message.Equals(""))
                {
                    foreach (var item in DataThirdType.RamaJudicial.Data)
                    {
                        string dispatch = item.despacho.ToString().ToLower();
                        DataRow rowBranchJudicial;
                        if (dispatch.Contains("administrativo"))
                        {
                            rowBranchJudicial = table_list_branch_judicial_administrative.NewRow();
                        }
                        else if (dispatch.Contains("civil"))
                        {
                            rowBranchJudicial = table_list_branch_judicial_civil.NewRow();
                        }
                        else if (dispatch.Contains("laboral"))
                        {
                            rowBranchJudicial = table_list_branch_judicial_labor.NewRow();
                        }
                        else if (dispatch.Contains("familia"))
                        {
                            rowBranchJudicial = table_list_branch_judicial_family.NewRow();
                        }
                        else if (dispatch.Contains("penal") || dispatch.Contains("con función de control de garantías") || dispatch.Contains("con función de conocimiento"))
                        {
                            rowBranchJudicial = table_list_branch_judicial_JEPMS.NewRow();
                        }
                        else
                        {
                            rowBranchJudicial = table_list_branch_judicial_other.NewRow();
                        }

                        rowBranchJudicial["idProcess"] = item.idProceso.ToString();
                        rowBranchJudicial["keyProcess"] = item.llaveProceso.ToString();
                        rowBranchJudicial["dateProcess"] = item.fechaProceso?.ToString();
                        rowBranchJudicial["dateLastPerformance"] = item.fechaUltimaActuacion?.ToString();
                        rowBranchJudicial["dispatch"] = item.despacho?.ToString();
                        rowBranchJudicial["department"] = item.departamento?.ToString();

                        if (item.sujetosProcesales != null && item.sujetosProcesales.ToString().Length > 300)
                            item.sujetosProcesales = item.sujetosProcesales.ToString().Substring(0, 300) + "(Ver mas con el numero de radicado)";
                        rowBranchJudicial["subjectsProcedural"] = item.sujetosProcesales?.ToString();

                        if (dispatch.Contains("administrativo"))
                        {
                            table_list_branch_judicial_administrative.Rows.Add(rowBranchJudicial);
                        }
                        else if (dispatch.Contains("civil"))
                        {

                            table_list_branch_judicial_civil.Rows.Add(rowBranchJudicial);
                        }
                        else if (dispatch.Contains("laboral"))
                        {

                            table_list_branch_judicial_labor.Rows.Add(rowBranchJudicial);
                        }
                        else if (dispatch.Contains("familia"))
                        {

                            table_list_branch_judicial_family.Rows.Add(rowBranchJudicial);
                        }
                        else if (dispatch.Contains("penal") || dispatch.Contains("con función de control de garantías") || dispatch.Contains("con función de conocimiento"))
                        {

                            table_list_branch_judicial_JEPMS.Rows.Add(rowBranchJudicial);
                        }
                        else
                        {
                            table_list_branch_judicial_other.Rows.Add(rowBranchJudicial);
                        }

                    }
                }
                ReportConsolidateNewBulkServicesAdditional.DataSources.Add(new ReportDataSource()
                {
                    Name = "list_branch_judicial_administrative",
                    Value = table_list_branch_judicial_administrative
                });
                ReportConsolidateNewBulkServicesAdditional.DataSources.Add(new ReportDataSource()
                {
                    Name = "list_branch_judicial_civil",
                    Value = table_list_branch_judicial_civil
                });
                ReportConsolidateNewBulkServicesAdditional.DataSources.Add(new ReportDataSource()
                {
                    Name = "list_branch_judicial_labor",
                    Value = table_list_branch_judicial_labor
                });
                ReportConsolidateNewBulkServicesAdditional.DataSources.Add(new ReportDataSource()
                {
                    Name = "list_branch_judicial_family",
                    Value = table_list_branch_judicial_family
                });
                ReportConsolidateNewBulkServicesAdditional.DataSources.Add(new ReportDataSource()
                {
                    Name = "list_branch_judicial_JEPMS",
                    Value = table_list_branch_judicial_JEPMS
                });
                ReportConsolidateNewBulkServicesAdditional.DataSources.Add(new ReportDataSource()
                {
                    Name = "list_branch_judicial_other",
                    Value = table_list_branch_judicial_other
                });

                var list_branch_judicial_jepms = new DataTable();
                list_branch_judicial_jepms.Columns.Add(new DataColumn("NameResult"));
                list_branch_judicial_jepms.Columns.Add(new DataColumn("IdentificationNumberResult"));
                list_branch_judicial_jepms.Columns.Add(new DataColumn("CityName"));
                list_branch_judicial_jepms.Columns.Add(new DataColumn("link"));

                if (DataThirdType.RamaJudicialJEMPS != null && rama_judicial_jepms_message.Equals(""))
                {
                    foreach (var item in DataThirdType.RamaJudicialJEMPS.Data)
                    {
                        DataRow rowBranchJudicial = list_branch_judicial_jepms.NewRow();
                        rowBranchJudicial["NameResult"] = item.NameResult != null ? item.NameResult.ToString() : "";
                        rowBranchJudicial["IdentificationNumberResult"] = item.IdentificationNumberResult != null ? item.IdentificationNumberResult.ToString() : "";
                        rowBranchJudicial["CityName"] = item.CityName != null ? item.CityName.ToString() : "";
                        rowBranchJudicial["Link"] = item.Link != null ? item.Link.ToString() : "";
                        list_branch_judicial_jepms.Rows.Add(rowBranchJudicial);
                    }
                }
                ReportConsolidateNewBulkServicesAdditional.DataSources.Add(new ReportDataSource()
                {
                    Name = "list_branch_judicial_jepms",
                    Value = list_branch_judicial_jepms
                });

                ReportConsolidateNewBulkServicesAdditional.ReportPath = contentRootPath + "\\Reports\\ReportConsolidateNewBulkServicesAdditional.rdlc";

                ReportConsolidateNewBulkServicesAdditional.SetParameters(parameters);
                ReportConsolidateNewBulkServicesAdditional.Refresh();

                ReportConsolidateNewBulkServicesAdditional.SubreportProcessing += SubreportProcessingEventHandler;

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;


                byte[] bytes = ReportConsolidateNewBulkServicesAdditional.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                PdfDocument tempPDFDoc = PdfReader.Open(new MemoryStream(bytes), PdfDocumentOpenMode.Import);

                for (int i = 0; i < tempPDFDoc.PageCount; i++)
                {
                    PdfPage page = tempPDFDoc.Pages[i];
                    document.AddPage(page);
                }
            }

            return document;
        }
        private Object[] createPdf(LocalReport report, HttpResponse response)
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            byte[] bytes = report.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            PdfDocument document = new PdfDocument();
            PdfDocument tempPDFDoc = PdfReader.Open(new MemoryStream(bytes), PdfDocumentOpenMode.Import);

            for (int i = 0; i < tempPDFDoc.PageCount; i++)
            {
                PdfPage page = tempPDFDoc.Pages[i];
                document.AddPage(page);
            }

            XFont font = new XFont("Verdana", 8);
            XBrush brush = XBrushes.Black;

            // Add the page counter.
            string noPages = document.Pages.Count.ToString();
            for (int i = 0; i < document.Pages.Count; ++i)
            {
                PdfPage page = document.Pages[i];

                // Make a layout rectangle.
                XRect layoutRectangle = new XRect(0/*X*/, page.Height - font.Height/*Y*/, page.Width/*Width*/, font.Height/*Height*/);

                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                {
                    gfx.DrawString(
                        "Pagina " + (i + 1).ToString() + " de " + noPages,
                        font,
                        brush,
                    layoutRectangle,
                        XStringFormats.Center);
                }
            }

            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            var options = document.Options;
            byte[] buffer = new byte[0];
            buffer = stream.ToArray();
            var contentLength = buffer.Length;
            response.Clear();
            response.ContentType = "application/pdf";
            stream.Close();
            string namepdf = "Reporte_" + System.DateTime.Now.ToString().Replace(" ", "_").Replace(",", "").Replace(".", "").Replace(":", "").Replace("/", "-");
            Object[] objects = new Object[4];
            objects[0] = bytes;
            objects[1] = mimeType;
            objects[2] = namepdf;
            objects[3] = extension;
            return objects;
        }
        private string settingToBase64(string img)
        {
            if (img.Contains("data"))
            {
                string im = img.Substring(22);
                return im;
            }
            else
            {
                return img;
            }

        }
    }
}
