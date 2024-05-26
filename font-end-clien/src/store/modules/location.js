import LocationApi from "@/api/location"
import { Message } from 'element-ui'

const state = {
    records: [],
    total: 0
}


const getters = {
    getLocations: (state) => state.records,
    getTotal: (state) => state.total
}


const mutations = {
    SET_RECORDS: (state, locations) => {
        state.records = locations
    },
    SET_TOTAL: (state, total) => {
        state.total = total
    },
}

const actions = {
    // get location
    async getList({ commit }, param) {
        return new Promise((resolve, reject) => {
            LocationApi.getList({ pageIndex: 0, pageSize: param.limit, keyword: param.keyword }).then(response => {
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
}

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions
}