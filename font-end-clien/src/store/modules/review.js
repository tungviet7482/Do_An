import Review from "@/api/review"
import { Message } from 'element-ui'

const state = {
    records: [],
    starRating: [],
    avgStars:0,
    total: 0
}


const getters = {
    getReviews: (state) => state.records,
    getStarRating: (state) => state.starRating,
    getAvgStars: (state) => state.avgStars,
    getTotal: (state) => state.total
}


const mutations = {
    SET_RECORDS: (state, data) => {
        state.records = data.reviews;
        state.starRating = data.starRating;
        state.avgStars = data.avgStars
    },
    SET_TOTAL: (state, total) => {
        state.total = total
    },
}

const actions = {
    // get reviews
    async getList({ commit }, param) {
        return new Promise((resolve, reject) => {

            Review.getList(param).then(response => {
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

                commit('SET_RECORDS', {reviews: data.data.items, starRating: data.data.starRating, avgStars: data.data.avgStars })
                commit('SET_TOTAL', data.data.totalRecord)
                return resolve(data.data)
            }).catch(error => {
                reject(error)
            })
        })
    },
    async create({ commit, dispatch }, param) {
        var data = await Review.create(param).then(response => {
            const data = response
            if (!data) {
                throw 'Verification failed, please Login again.'
            }
            if (!data.status) {
                return
            }
            return data
        }).catch(error => {
            throw error
        })
        return data;
    },
}

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions
}