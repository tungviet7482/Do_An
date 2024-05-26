import UserApi from '@/api/user'
import { getToken, setToken, removeToken } from '@/utils/auth'
import router, { resetRouter } from '@/router'
import { Message } from 'element-ui'

const state = {
  token: getToken(),
  accountInfo: {
    id: null,
    fullName: null,
    roles: [],
    companyId: null,
    fileCVUrl: null,
    avatarUrl: null
  },
  companyInfo: null
}

const getters = {
  getAccountInfo: (state) => state.accountInfo,
  getCompanyInfo: (state) => state.companyInfo
}

const mutations = {
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_ACCOUNT: (state, account) => {
    state.accountInfo = account
  },
  SET_COMPANY: (state, company) => {
    state.companyInfo = company
  },
  SET_ROLES: (state, roles) => {
    state.accountInfo.roles = [roles]
  },
  SET_CVURL: (state, url) => {
    state.accountInfo.cvUrl = url
  },
  RESET_ACOUNT: (state) => {
    state.token = null;
    state.accountInfo.id = null;
    state.accountInfo.fullName = null;
    state.accountInfo.roles = "";
    state.accountInfo.companyId = null;
    state.accountInfo.fileCVUrl = null;
    state.accountInfo.avatarUrl = null;
  }
}

const actions = {
  async login({ commit, dispatch }, param) {
    const { email, password } = param
    return new Promise((resolve, reject) => {
      UserApi.Login({ email, password }).then(data => {
        if (!data.status) {
          return {
            Message: data.Message,
            Errors: data.Error
          }
        }

        if (data.data.role.includes('admin')) {
          Message({
            message: 'Tài khoản không có quyền truy cập',
            type: 'error',
            duration: 4 * 1000
          })
          return resolve()
        }
        commit('SET_TOKEN', data.data.token)
        commit('SET_ROLES', data.data.role)
        setToken(data.data.token)
        dispatch('getInfo')
        resolve(data)
      }).catch(error => {
        reject(error)
      })
    })
  },
  async register({ commit, dispatch }, param) {

    return new Promise((resolve, reject) => {
      UserApi.register(param).then(async data => {
        if (!data.status) {
          Message({
            message: data.error.errors || "Lỗi một số trường nhập",
            type: 'error',
            duration: 3 * 1000
          })
          return {
            Message: data.Message,
            Errors: data.Error
          }
        }
        var response = await dispatch('login', param)
        if (response.status && param.roles.includes('user')) {
          router.push({ name: 'HomePage' })
        }
        else {
          router.push({ name: 'HomePageRecruitment' })
        }
        resolve(data)
      }).catch(error => {
        reject(error)
      })
    })
  },
  uploadCV({ commit }, param) {
    const { id, url } = param
    return new Promise((resolve, reject) => {
      UserApi.uploadCV(id).then(data => {
        if (!data.status) {
          return {
            Message: data.Message,
            Errors: data.Error
          }
        }
        commit('SET_CVURL', url)
        resolve(data)
      }).catch(error => {
        reject(error)
      })
    })
  },
  // get user info
  getInfo({ commit }) {
    return new Promise((resolve, reject) => {
      UserApi.getInfo().then(response => {
        const { data } = response

        if (!data) {
          reject('Verification failed, please Login again.')
        }
        // const { roles, fullName, avatarUrl, companyId, fileCVUrl, introduction } = data

        // roles must be a non-empty array
        // if (!roles || roles.length <= 0) {
        //   reject('getInfo: roles must be a non-null array!')
        // }

        commit('SET_ACCOUNT', data)
        resolve(data)
      }).catch(error => {
        reject(error)
      })
    })
  },

  async getCompany({ commit }, param) {
    return new Promise((resolve, reject) => {
      UserApi.getCompanies(param).then(response => {
        const data = response
        if (!data) {
          reject('Verification failed, please Login again.')
        }
        if (!data.status) {
          Message({
            message: data.message,
            type: 'error',
            duration: 4 * 1000
          })
          return resolve()
        }

        commit('SET_COMPANY', data.data.items[0])
        return resolve(data.data)
      }).catch(error => {
        reject(error)
      })
    })
  },

  update({ commit }, param) {
    return new Promise((resolve, reject) => {
      UserApi.update(param).then(response => {
        const data = response
        if (!data) {
          reject('Verification failed, please Login again.')
        }
        if (!data.status) {
          return resolve()
        }
        commit('SET_ACCOUNT', param)
        return resolve(data)
      }).catch(error => {
        reject(error)
      })
    })
  },

  // user logout
  logout({ commit, state, dispatch }) {
    return new Promise((resolve, reject) => {
      UserApi.logout(state.token).then(() => {
        commit('RESET_ACOUNT')
        removeToken()
        resetRouter()
        dispatch('tagsView/delAllViews', null, { root: true })
        router.push({ name: 'HomePage' })
        resolve()
      }).catch(error => {
        debugger
        reject(error)
      })
    })
  },

  // remove token
  resetToken({ commit }) {
    return new Promise(resolve => {
      commit('RESET_ACOUNT')
      removeToken()
      resolve()
    })
  },

  // dynamically modify permissions
  async changeRoles({ commit, dispatch }, role) {
    const token = role + '-token'

    commit('SET_TOKEN', token)
    setToken(token)

    const { roles } = await dispatch('getInfo')

    resetRouter()

    // generate accessible routes map based on roles
    const accessRoutes = await dispatch('permission/generateRoutes', roles, { root: true })
    // dynamically add accessible routes
    router.addRoutes(accessRoutes)

    // reset visited views and cached views
    dispatch('tagsView/delAllViews', null, { root: true })
  }
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}
