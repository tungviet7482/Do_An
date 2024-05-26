import ApiClient from "./ApiClient";

class Company extends ApiClient{
  constructor(){
    super({controller: "companies"});
  }

  getCompanies(param) {
    return axios({ url: `${this.baseUrl()}`, method: 'get', params: param })
  }

  getCompanyBySlug(slug) {
    return axios({ url: `${this.baseUrl()}/${slug}`, method: 'get' })
  }
}

export default new Company();