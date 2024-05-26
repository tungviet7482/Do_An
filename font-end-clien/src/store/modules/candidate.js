import UserApi from '@/api/user'
import { Message } from 'element-ui'

const state = {
    records: [],
    total: 0
}

const getters = {
    getCandidates: (state) => state.records,
    getTotal: (state) => state.total,
}

const mutations = {
    SET_RECORDS: (state, candidates) => {
        state.records = candidates
    },
    SET_TOTAL: (state, total) => {
        state.total = total
    },
    DELETE_CANDIDATE: (state, id) => {
        let indexObj = state.records.findIndex(obj => obj.id === id);
        if (indexObj !== -1) {
            state.records.splice(indexObj, 1);
            console.log(state.records)
            state.total--;
        }
    }
}

const actions = {
    // get candidate
    async getList({ commit }, param) {
        return new Promise((resolve, reject) => {
            UserApi.GetPagingCandidates(param).then(response => {
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

    async getListApplied({ commit }, param) {
        return new Promise((resolve, reject) => {
            UserApi.GetPagingCandidatesApplied(param).then(response => {
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

    async candidateClassification({ commit }, param) {
        return new Promise((resolve, reject) => {
            UserApi.candidateClassification(param).then(response => {
                const data = response
                if (!data) {
                    reject('Verification failed, please Login again.')
                }
                // if(!data.data){
                //     return resolve();
                // }
                if (!data.status) {
                    Message({
                        message: data.message,
                        type: 'error',
                        duration: 4 * 1000
                    })
                    return resolve()
                }
                if(param.interested != null)
                {
                    commit('DELETE_CANDIDATE', param.userId)
                }
                return resolve(data.data)
            }).catch(error => {
                reject(error)
            })
        })
    },
}

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions
}