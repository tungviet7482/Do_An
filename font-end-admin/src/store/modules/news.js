import NewsApi from "@/api/news"
import { Message } from 'element-ui'

const state = {
    records: [],
    total: 0,
}


const getters = {
    getNews: (state) => state.records,
    getNewsById: (state) => id => {
        debugger
        var news = state.records.find(x => x.id == id);
        return news
    },
    getTotal: (state) => state.total,
}


const mutations = {
    SET_RECORD: (state, news) => {
        state.records = news
    },
    SET_TOTAL: (state, total) => {
        state.total = total
    },
    UPDATE_RECORD: (state, param) => {
        let obj = state.records.find(x => x.id === param.id);
        if (obj) {
            obj = param
        }
    },

    DELETE_RECORD: (state, id) => {
        let indexObj = state.records.findIndex(obj => obj.id === id);
        if (indexObj !== -1) {
            state.records.splice(indexObj, 1);
            state.totalNews--;
        }
    },
}

const actions = {
    // get User
    async getNews({ commit }, param) {
        return new Promise((resolve, reject) => {
            NewsApi.getList(param).then(response => {
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

                commit('SET_RECORD', data.data.items)
                commit('SET_TOTAL', data.data.totalRecord)
                return resolve(data.data)
            }).catch(error => {
                reject(error)
            })
        })
    },
    async createNews({ commit, dispatch }, param) {

        var data = await NewsApi.create(param).then(response => {
            const data = response
            if (!data) {
                throw 'Verification failed, please Login again.'
            }
            if (!data.status) {
                return
            }
            dispatch('getNews', { page: 1, limit: 10 })
            return data
        }).catch(error => {
            throw error
        })
        return data;
    },
    updateNews({ commit }, param) {
        return new Promise((resolve, reject) => {
            NewsApi.update(param).then(response => {
                const data = response
                if (!data) {
                    reject('Verification failed, please Login again.')
                }
                if (!data.status) {
                    return resolve()
                }
                commit('UPDATE_RECORD', param)
                return resolve(data)
            }).catch(error => {
                reject(error)
            })
        })
    },
    async delete({ commit }, param) {
        return new Promise((resolve, reject) => {

            NewsApi.delete(param).then(response => {
                const data = response
                if (!data) {
                    reject('Verification failed, please Login again.')
                }
                if (!data.status) {
                    return resolve()
                }
                commit('DELETE_RECORD', param.id);
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