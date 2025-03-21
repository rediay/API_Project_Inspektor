using Amazon.Runtime;
using Common.Services.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Configuration;
using Common.Entities.SPsData.AditionalServices.Procuraduria;
using Common.Entities.SPsData.AditionalServices;
using Newtonsoft.Json;
using Common.Entities.SPsData.AditionalServices.RamaJudicial;
using System.IO;
using System.Net;
using Common.Entities.SPsData.AditionalServices.RegistryDeaths;
using Common.Entities.Helpers;
using Common.Entities;
using Common.Entities.SPsData.AditionalServices.Simit;
using Common.DTO;
using Newtonsoft.Json.Linq;
using Common.Entities.SPsData.AditionalServices.Rues;
using System.Text.RegularExpressions;
using Common.Entities.SPsData.AditionalServices.EPS;
using Common.Entities.SPsData.AditionalServices.PPT;
using Common.Entities.SPsData.AditionalServices.CriminalRecordEcuador;
using Common.Entities.SPsData.AditionalServices.JudicialInformationEcuador;
using System.Web.Helpers;
using Common.Entities.SPsData.AditionalServices.Sunat;
using Common.Entities.SPsData.AditionalServices.Police;
using Common.Entities.SPsData.AditionalServices.Bme;

namespace Common.DataAccess.EFCore.Repositories.RequestHelper
{
    public class RequestRepository
    {
        protected IConfigurationSection configuration;
        protected readonly HttpClient _httpClient;
        public RequestRepository(Utils.HttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            this.configuration = configuration.GetSection("webservicesOptions");
        }
        public async Task<cXsHttpResponse<Procuraduria>> makeProcuraduriaRequest(string identification, int typeDocument)
        {
            string url = $"{this.configuration["procUrl"]}{typeDocument}&NumeroIdentificacion={identification}";

            cXsHttpResponse<Procuraduria> procuraduria = new cXsHttpResponse<Procuraduria>();
            try
            {
                RequestAditionalServiceParams parameters = new RequestAditionalServiceParams()
                {
                    url = url,
                    method = HttpMethod.Get
                };
                var json = await MakeHttpRequest(parameters);
                json = json.Replace(",\"not_criminal_records\":false", "");
                procuraduria.Data = JsonConvert.DeserializeObject<Procuraduria>(json);
            }
            catch (Exception ex)
            {
                procuraduria.HasError = true;
                procuraduria.ErrorMessage = this.configuration["serviceError"];
                return procuraduria;
            }
            return procuraduria;
        }
        public async Task<cXsHttpResponse<IEnumerable<RamaJudicial>>> makeRamaJudicialRequest(string name)
        {

            string url = $"{this.configuration["ramaUrl"] + name}";
            cXsHttpResponse<IEnumerable<RamaJudicial>> ramaJudicials = new cXsHttpResponse<IEnumerable<RamaJudicial>>();

            try
            {
                RequestAditionalServiceParams parameters = new RequestAditionalServiceParams()
                {
                    url = url,
                    method = HttpMethod.Get,
                    token = this.configuration["ramaProcToken"]
                };
                var json = await MakeHttpRequest(parameters);

                JArray jsonArray = JArray.Parse(json);
                string message = (string)jsonArray[0]["message"];

                if (message != null)
                {
                    ramaJudicials.HasError = true;
                    ramaJudicials.ErrorMessage = message;
                    return ramaJudicials;
                }

                ramaJudicials.Data = JsonConvert.DeserializeObject<List<RamaJudicial>>(json);

            }
            catch (Exception ex)
            {
                ramaJudicials.HasError = true;
                ramaJudicials.ErrorMessage = this.configuration["serviceError"];
                return ramaJudicials;
            }
            return ramaJudicials;
        }
        public async Task<cXsHttpResponse<IEnumerable<RamaJudicialJEMPS>>> makeRamaJudicialJEMPSRequest(string document)
        {
            string url = $"{this.configuration["ramaUrlJEMPS"] + document}";

            cXsHttpResponse<IEnumerable<RamaJudicialJEMPS>> ramaJudicials = new cXsHttpResponse<IEnumerable<RamaJudicialJEMPS>>();

            try
            {
                RequestAditionalServiceParams parameters = new RequestAditionalServiceParams()
                {
                    url = url,
                    method = HttpMethod.Get,
                    token = this.configuration["ramaProcToken"]
                };
                var json = await MakeHttpRequest(parameters);
                ramaJudicials.Data = JsonConvert.DeserializeObject<List<RamaJudicialJEMPS>>(json);

            }
            catch (Exception ex)
            {
                ramaJudicials.HasError = true;
                ramaJudicials.ErrorMessage = this.configuration["serviceError"];
                return ramaJudicials;
            }
            return ramaJudicials;
        }
        public async Task<cXsHttpResponse<cInfoDefRegistraduria>> makeRegistryDeaths(string document)
        {
            string url = this.configuration["RegistryDeathsUrl"];
            cXsHttpResponse<cInfoDefRegistraduria> DefRegistraduria = new cXsHttpResponse<cInfoDefRegistraduria>();

            try
            {

                var json = new DefRegistraduriaPostObject()
                {
                    nuip = document,
                    ip = "0.0.0."
                };
                using (var content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"))
                {
                    RequestAditionalServiceParams parameters = new RequestAditionalServiceParams()
                    {
                        url = url,
                        method = HttpMethod.Post,
                        content = content
                    };
                    string returnValue = await MakeHttpRequest(parameters);
                    if (returnValue.Contains("Error"))
                    {
                        dynamic jsonError = JsonConvert.DeserializeObject(returnValue);
                        string error = jsonError.error;
                        DefRegistraduria.HasError = true;
                        DefRegistraduria.ErrorMessage = error;
                    }
                    else
                    {
                        DefRegistraduria.Data = JsonConvert.DeserializeObject<cInfoDefRegistraduria>(returnValue);
                    }
                    return DefRegistraduria;

                }

            }
            catch (Exception ex)
            {
                DefRegistraduria.HasError = true;
                DefRegistraduria.ErrorMessage = this.configuration["serviceError"];
                return DefRegistraduria;
            }

        }
        public async Task<cXsHttpResponse<Military>> makeMiltaryRequest(string identification)
        {
            var token = configuration["militaryToken"];
            string url = $"{configuration["militaryUrl"]}{identification}&License={token}";
            cXsHttpResponse<Military> military = new cXsHttpResponse<Military>();

            try
            {
                RequestAditionalServiceParams parameters = new RequestAditionalServiceParams()
                {
                    url = url,
                    method = HttpMethod.Get,
                    token = token
                };
                var json = await MakeHttpRequest(parameters);

                military.Data = JsonConvert.DeserializeObject<Military>(json);


            }
            catch (Exception ex)
            {
                military.HasError = true;
                military.ErrorMessage = configuration["serviceError"];
                return military;
            }
            return military;
        }
        public async Task<cXsHttpResponse<List<cInfoSimitNew>>> makeSimit(string document)
        {
            cXsHttpResponse<List<cInfoSimitNew>> simit = new cXsHttpResponse<List<cInfoSimitNew>>();
            string url = configuration["simitUrl"];

            try
            {
                var json = new SimitPostObject()
                {
                    cedula = document,
                };
                using (var content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"))
                {
                    RequestAditionalServiceParams parameters = new RequestAditionalServiceParams()
                    {
                        url = url,
                        method = HttpMethod.Post,
                        content = content
                    };
                    var data = await MakeHttpRequest(parameters);
                    simit = JsonConvert.DeserializeObject<cXsHttpResponse<List<cInfoSimitNew>>>(data);
                    if (simit.Data.Count == 0)
                    {
                        simit.ErrorMessage = configuration["serviceErrorSimit"];
                    }
                    return simit;
                }
            }
            catch (Exception ex)
            {
                simit.HasError = true;
                simit.ErrorMessage = configuration["serviceError"];
                return null;
            }
        }

        public async Task<cXsHttpResponse<Rues>> makeRuesRequest(string document)
        {
            var myHttpClient = new HttpClient();
            cXsHttpResponse<Rues> rues = new cXsHttpResponse<Rues>();
            string url = configuration["ruesUrl"];
            var response1 = myHttpClient.GetAsync(url);
            var doc = response1.Result.Content.ReadAsStringAsync().Result;
            var regex = new Regex("value=\"([^\"]+)");
            string myCapturedText = string.Empty;
            try
            {
                if (regex.IsMatch(doc))
                {
                    myCapturedText = regex.Match(doc).Groups[1].Value;
                }
                var formContent = new FormUrlEncodedContent(new[] {

                    new KeyValuePair<string, string>("txtNIT",document),
                    new KeyValuePair<string, string>("__RequestVerificationToken", myCapturedText)
                });
                var response2 = myHttpClient.PostAsync(url, formContent);
                rues = RuesTocInfoRues(JsonConvert.DeserializeObject<cRuesJson>(response2.Result.Content.ReadAsStringAsync().Result));
                return rues;


            }
            catch (Exception ex)
            {
                rues.HasError = true;
                rues.ErrorMessage = configuration["serviceError"];
                return rues;
            }
            finally
            {
                myHttpClient.Dispose();
            }
        }
        private cXsHttpResponse<Rues> RuesTocInfoRues(cRuesJson rues)
        {
            try
            {
                cXsHttpResponse<Rues> obj = new cXsHttpResponse<Rues>();

                obj.HasError = Convert.ToBoolean(Convert.ToInt32(rues.codigo_error));
                obj.ErrorMessage = rues.mensaje_error;
                if (rues.rows != null)
                {
                    obj.ListData = new List<Rues>();
                    foreach (var data in rues.rows)
                    {
                        obj.ListData.Add(
                            new Rues
                            {
                                RazonSocialONombre = data.razon_social,
                                municipio = data.municipio,
                                Categoria = data.matricula,
                                Estado = data.estado,
                                Nit = data.identificacion
                            }
                            );
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<SuperSocieties> makeSuperSocietiesRequest(string identification)
        {
            string url = $"{configuration["superSocietiesUrl"]}{identification}";
            SuperSocieties superSocieties = new SuperSocieties();
            try
            {
                RequestAditionalServiceParams parameters = new RequestAditionalServiceParams()
                {
                    url = url,
                    method = HttpMethod.Get,
                };
                var json = await MakeHttpRequest(parameters);

                superSocieties = JsonConvert.DeserializeObject<SuperSocieties>(json);

                if (superSocieties.data == null)
                {
                    superSocieties.ErrorMessage = configuration["serviceErrorSuperSocieties"];
                }
            }
            catch (Exception ex)
            {
                superSocieties.HasError = true;
                superSocieties.ErrorMessage = configuration["serviceError"];
                return superSocieties;
            }
            return superSocieties;
        }
        public async Task<cXsHttpResponse<Eps>> makeConsultaEPS(string document)
        {
            string url = configuration["epsUrl"] + document;
            cXsHttpResponse<Eps> eps = new cXsHttpResponse<Eps>();
           
            try
            {
              
                RequestAditionalServiceParams parameters = new RequestAditionalServiceParams()
                {
                    url = url,
                    method = HttpMethod.Get
                };
                var jsonStringsign = await MakeHttpRequest(parameters);
                var obj = JsonConvert.DeserializeObject<cEPSJson>(jsonStringsign);
                eps.ListData = obj.rows.ToList();
                //eps = epsTocInfoEps(JsonConvert.DeserializeObject<cEPSJson>(jsonStringsign));
                return eps;

            }
            catch (Exception ex)
            {
                eps.HasError = true;
                eps.ErrorMessage = configuration["serviceError"];
                return eps;
            }
        }
        public async Task<cXsHttpResponse<Ppt>> makeConsultaPPT(int TipoIden, string NumeIden)
        {
            string url = configuration["pptUrl"] + "&searchType=" + TipoIden + "&searchParam=" + NumeIden.ToString();
            var TokenProc = configuration["ramaProcToken"];
            cXsHttpResponse<Ppt> ppt = new cXsHttpResponse<Ppt>();

            try
            {

                RequestAditionalServiceParams parameters = new RequestAditionalServiceParams()
                {
                    url = url,
                    method = HttpMethod.Get,
                    token = TokenProc,
                };
                var jsonStringsign = await MakeHttpRequest(parameters);
                ppt.Data = JsonConvert.DeserializeObject<Ppt>(jsonStringsign);
                return ppt;

            }
            catch (Exception ex)
            {
                ppt.HasError = true;
                ppt.ErrorMessage = configuration["serviceError"];
                return ppt;
            }
        }
        //private cXsHttpResponse<Eps> epsTocInfoEps(cEPSJson eps)
        //{
        //    try
        //    {
        //        cXsHttpResponse<Eps> obj = new cXsHttpResponse<Eps>();

        //        obj.HasError = Convert.ToBoolean(eps.codigo_error);
        //        obj.ErrorMessage = eps.mensaje_error;
        //        if (eps.rows != null)
        //        {
        //            var data = eps.rows;
        //            obj.Data = new Eps
        //            {
        //                State = data.State,
        //                Entity = data.Entity,
        //                Regime = data.Regime,
        //                EffectiveDate = data.EffectiveDate,
        //                EndDate = data.EndDate,
        //                AffiliateType = data.AffiliateType,
        //            };
        //        }

        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public async Task<cXsHttpResponse<CriminalRecordEcuador>> makeConsultaCriminalRecordEcuador(string NumeIden)
        {
            string url = configuration["CriminalRecordEcuadorUrl"];
            cXsHttpResponse<CriminalRecordEcuador> criminalRecordEcuador = new cXsHttpResponse<CriminalRecordEcuador>();


            try
            {

                string json = JsonConvert.SerializeObject(new
                {
                    cedula = NumeIden
                });
                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    RequestAditionalServiceParams parameters = new RequestAditionalServiceParams()
                    {
                        url = url,
                        method = HttpMethod.Post,
                        content = content
                    };
                    string jsonStringsign = await MakeHttpRequest(parameters);
                    criminalRecordEcuador.Data = JsonConvert.DeserializeObject<CriminalRecordEcuador>(jsonStringsign);
                    return criminalRecordEcuador;
                }
            }
            catch (Exception ex)
            {
                criminalRecordEcuador.HasError = true;
                criminalRecordEcuador.ErrorMessage = configuration["serviceError"];
                return criminalRecordEcuador;
            }
        }

        public async Task<cXsHttpResponse<List<InfoJudicial>>> makeConsultaJudicialInformationEcuador(string NumeIden)
        {
            string url = configuration["JudicialInformationEcuadorUrl"];

            cXsHttpResponse<List<InfoJudicial>> judicialEcuador = new cXsHttpResponse<List<InfoJudicial>>();
            try
            {


                string json = JsonConvert.SerializeObject(new
                {
                    cedula = NumeIden
                });
                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                {

                    RequestAditionalServiceParams parameters = new RequestAditionalServiceParams()
                    {
                        url = url,
                        method = HttpMethod.Post,
                        content = content
                    };
                    string jsonStringsign = await MakeHttpRequest(parameters);
                    if (jsonStringsign.Contains("TimeoutError"))
                    {
                        judicialEcuador.HasError = true;
                        judicialEcuador.ErrorMessage = configuration["serviceError"];
                    }
                    else
                    {
                        judicialEcuador = JsonConvert.DeserializeObject<cXsHttpResponse<List<InfoJudicial>>>(jsonStringsign);
                    }

                    return judicialEcuador;

                }
            }
            catch (Exception ex)
            {
                judicialEcuador.HasError = true;
                judicialEcuador.ErrorMessage = configuration["serviceError"];
                return judicialEcuador;
            }
        }
        public async Task<cXsHttpResponse<Sunat>> makeConsultaSunat(string document)
        {
            string url = configuration["sunatUrl"];
            cXsHttpResponse<Sunat> sunat = new cXsHttpResponse<Sunat>();
            try
            {
                string json = JsonConvert.SerializeObject(new
                {
                    doc = document
                });
                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    RequestAditionalServiceParams parameters = new RequestAditionalServiceParams()
                    {
                        url = url,
                        method = HttpMethod.Post,
                        content = content
                    };
                    string jsonStringsign = await MakeHttpRequest(parameters);
                    jsonStringsign = jsonStringsign.Replace("[]", "null");
                    sunat = sunatTocInfosunat(JsonConvert.DeserializeObject<cSunatJson>(jsonStringsign));
                    return sunat;

                }
            }
            catch (Exception ex)
            {
                sunat.HasError = true;
                sunat.ErrorMessage = configuration["serviceError"];
                return sunat;
            }
        }
        public async Task<cXsHttpResponse<Police>> makePoliceRequest(string document, int typeDocument)
        {
            string url = $"{configuration["policeUrl"]}&documentType={typeDocument}&documentNumberPolNal={document}";
            string token = configuration["policeToken"];
            cXsHttpResponse<Police> police = new cXsHttpResponse<Police>();
            try
            {

                RequestAditionalServiceParams parameters = new RequestAditionalServiceParams()
                {
                    url = url,
                    method = HttpMethod.Get,
                    token = token,
                };
                var jsonStringsign = await MakeHttpRequest(parameters);
                police.Data = JsonConvert.DeserializeObject<Police>(jsonStringsign);
                return police;
            }
            catch (Exception ex)
            {
                police.HasError = true;
                police.ErrorMessage = configuration["serviceError"];
                return police;
            }
        }
        public async Task<cXsHttpResponse<Bme>> makeBmeRequest(string document, int typeDocument)
        {
            string url = $"{configuration["bmeUrl"]}&type_document={typeDocument}&cedula={document}";
            cXsHttpResponse<Bme> bme = new cXsHttpResponse<Bme>();
            try
            {

                RequestAditionalServiceParams parameters = new RequestAditionalServiceParams()
                {
                    url = url,
                    method = HttpMethod.Get,
                };
                var jsonStringsign = await MakeHttpRequest(parameters);
                bme.Data = JsonConvert.DeserializeObject<Bme>(jsonStringsign);
                return bme;
            }
            catch (Exception ex)
            {
                bme.HasError = true;
                bme.ErrorMessage = configuration["serviceError"];
                return bme;
            }
        }
        private cXsHttpResponse<Sunat> sunatTocInfosunat(cSunatJson sunat)
        {
            try
            {
                cXsHttpResponse<Sunat> obj = new cXsHttpResponse<Sunat>();

                obj.HasError = sunat.mensaje_error != null ? true : Convert.ToBoolean(sunat.codigo_error);
                obj.ErrorMessage = sunat.mensaje_error;
                if (sunat.rows != null)
                {
                    var data = sunat.rows;
                    obj.Data = new Sunat
                    {
                        no_ruc = data.no_ruc,
                        tipo_cont = data.tipo_cont,
                        nombre_comercial = data.nombre_comercial,
                        fecha_inscripcion = data.fecha_inscripcion,
                        fecha_inicio_actividades = data.fecha_inicio_actividades,
                        estado = data.estado,
                        condicion = data.condicion,
                        domicilio = data.domicilio,
                        sistema_emicion = data.sistema_emicion,
                        actividad_comercio_exterior = data.actividad_comercio_exterior,
                        sistema_contabilidad = data.sistema_contabilidad,
                        actividades_economicas = data.actividades_economicas,
                        comprobante_pago = data.comprobante_pago,
                        sistema_emision_electronica = data.sistema_emision_electronica,
                        emision_electronica_desde = data.emision_electronica_desde,
                        comprobantes_electronicos = data.comprobantes_electronicos,
                        afiliado_ple = data.afiliado_ple,
                        padrones = data.padrones,
                    };
                }

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> MakeHttpRequest(RequestAditionalServiceParams parameters)
        {
            var request = new HttpRequestMessage(parameters.method, parameters.url);

            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            if (!string.IsNullOrEmpty(parameters.token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", parameters.token);
                request.Headers.Authorization = new AuthenticationHeaderValue(parameters.token);
            }

            if (parameters.content != null)
            {
                request.Content = parameters.content;
            }

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }

            return null;
        }



    }
}
