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
}

const actions = {
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
    getNewsBySlug({ commit },slug){
        return new Promise((resolve, reject) => {
            NewsApi.getNewsBySlug(slug).then(response => {
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