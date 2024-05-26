import UserApi from "@/api/user"
import { Message } from 'element-ui'

const state = {
    records: {
        candidates: [],
        companies: []
    },
    totalCompany: 0,
    totalCandidate: 0
}


const getters = {
    getCompanies: (state) => state.records.companies,
    getCompanyById: (state) => id => {
        var company = state.records.companies.find(x => x.id == id);
        return company
    },
    getCandidates: (state) => state.records.candidates,
    getCandidateById: (state) => id => {
        var candidate = state.records.candidates.find(x => x.id == id);
        return candidate
    },
    getTotalCompanies: (state) => state.totalCompany,
    getTotalCandidates: (state) => state.totalCandidate,
}


const mutations = {
    SET_RECORDS_COMPANY: (state, companies) => {
        state.records.companies = companies
    },
    SET_RECORDS_CANDIDATE: (state, candidates) => {
        state.records.candidates = candidates
    },
    SET_TOTAL_COMPANY: (state, total) => {
        state.total = total
    },
    SET_TOTAL_CANDIDATE: (state, total) => {
        state.total = total
    },
    UPDATE_RECORDS_COMPANY: (state, param) => {
        let obj = state.records.companies.find(x => x.id === param.id);
        if (obj) {
            obj = param
        }
    },
    UPDATE_RECORDS_CANDIDATE: (state, param) => {
        let obj = state.records.find(x => x.id === param.id);

        if (obj) {
            obj.name = param.name;
            obj.updateAt = new Date();
        }
    },
    DELETE_COMPANY: (state, id) => {
        debugger
        let indexObj = state.records.companies.findIndex(obj => obj.id === id);
        if (indexObj !== -1) {
            state.records.companies.splice(indexObj, 1);
            state.totalCompany--;
        }
    },
    DELETE_CANDIDATE: (state, id) => {
        let indexObj = state.records.candidate.findIndex(obj => obj.id === id);
        if (indexObj !== -1) {
            state.records.candidate.splice(indexObj, 1);
            console.log(state.records)
            state.totalCandidate--;
        }
    }
}

const actions = {
    // get User
    async getCompanies({ commit }, param) {
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

                commit('SET_RECORDS_COMPANY', data.data.items)
                commit('SET_TOTAL_COMPANY', data.data.totalRecord)
                return resolve(data.data)
            }).catch(error => {
                reject(error)
            })
        })
    },
    async getCandidates({ commit }, param) {
        return new Promise((resolve, reject) => {
            UserApi.getCandidates(param).then(response => {
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

                commit('SET_RECORDS_CANDIDATE', data.data.items)
                commit('SET_TOTAL_CANDIDATE', data.data.totalRecord)
                return resolve(data.data)
            }).catch(error => {
                reject(error)
            })
        })
    },
    async create({ commit, dispatch }, param) {

        var data = await UserApi.create(param).then(response => {
            const data = response
            if (!data) {
                throw 'Verification failed, please Login again.'
            }
            if (!data.status) {
                return
            }
            dispatch('getCompanies', { page: 1, limit: 10 })
            return data
        }).catch(error => {
            throw error
        })
        return data;
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
                commit('UPDATE_RECORDS_COMPANY', param)
                return resolve(data)
            }).catch(error => {
                reject(error)
            })
        })
    },
    async delete({ commit }, param) {
        return new Promise((resolve, reject) => {
            // debugger
            var dataRequest = {
                id: param.id,
            }
            UserApi.delete(dataRequest).then(response => {
                const data = response
                if (!data) {
                    reject('Verification failed, please Login again.')
                }
                if (!data.status) {
                    return resolve()
                }
                debugger
                if (param.roles[0] == 'company') {
                    commit('DELETE_COMPANY', dataRequest.id);
                }
                else{
                    commit('DELETE_CANDIDATE', dataRequest.id);
                }
                return resolve(data)
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