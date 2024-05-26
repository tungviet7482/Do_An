import CompanyApi from "@/api/company"
import { Message } from 'element-ui'

const state = {
    records: [],
    total: 0
}


const getters = {
    getCompanies: (state) => state.records,
    getTotal: (state) => state.total,
    getCompanyBySlug: (state) => slug => {
        return state.records.find(x => x.slug == slug)
    }
}


const mutations = {
    SET_RECORDS: (state, companies) => {
        state.records = companies
    },
    SET_TOTAL: (state, total) => {
        state.total = total
    },
}

const actions = {
    // get Company
    async getList({ commit }, param) {
        return new Promise((resolve, reject) => {
            CompanyApi.getCompanies(param).then(response => {
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

                commit('SET_RECORDS', data.data.items)
                commit('SET_TOTAL', data.data.totalRecord)
                return resolve(data.data)
            }).catch(error => {
                reject(error)
            })
        })
    },

    getCompanyBySlug({ commit },slug){
        return new Promise((resolve, reject) => {
            CompanyApi.getCompanyBySlug(slug).then(response => {
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
                return resolve(data.data)
            }).catch(error => {
                reject(error)
            })
        })
    }
}

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions
}