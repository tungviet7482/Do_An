import ApiClient from "./ApiClient";

class Auth extends ApiClient{
  constructor(){
    super({controller: "users"});
  }

  // Login(param){
  //   return axios.post(`${this.baseUrl()}/login`, param);
  // }

  Login(param){
    return axios({url:`${this.baseUrl()}/login`, method:'post', data: param})
  }

  getInfo() {
      return axios({url:`${this.baseUrl()}/user-infor`, method:'get'})
    }
    
  logout() {
    return axios({url:`${this.baseUrl()}/logout`, method:'post'})
    }
}

export default new Auth();