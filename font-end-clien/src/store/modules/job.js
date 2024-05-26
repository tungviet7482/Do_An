import JobApi from "@/api/job"
import router, { resetRouter } from '@/router'
import { Message } from 'element-ui'

const state = {
    records: {
        activeJobs: [],
        expiredJobs: [],
        savedJobs: [],
        jobsInCompany: [],
        appliedJobs: []
    },
    totalActiveJobs: 0,
    totalExpiredJobs: 0,
    totalJobsInCompany: 0,
    totalSavedJobs: 0,
    totalAppliedJobs: 0,
}


const getters = {
    getActiveJobs: (state) => state.records.activeJobs,
    getExpiredJobs: (state) => state.records.expiredJobs,
    getJobsInCompany: (state) => state.records.jobsInCompany,
    getAppliedJobs: (state) => state.records.appliedJobs,
    getTotalActiveJobs: (state) => state.totalActiveJobs,
    getTotalExpiredJobs: (state) => state.totalExpiredJobs,
    getTotalJobsInCompany: (state) => state.totalJobsInCompany,
    getTotalAppliedJobs: (state) => state.totalAppliedJobs,
    getActiveJobById: (state) => id => {
        var job = state.records.activeJobs.find(x => x.id == id);
        return job
    },
    getSavedJobs: (state) => state.records.savedJobs,
    getTotalSavedJobs: (state) => state.totalSavedJobs,
    getJobBySlug: (state) => slug => {
        return state.records.activeJobs.find(x => x.slug == slug)
    }
}


const mutations = {
    SET_RECORDS_ACTIVEJOBS: (state, jobs) => {
        state.records.activeJobs = jobs
    },
    SET_RECORDS_EXPIREDJOBS: (state, jobs) => {
        state.records.expiredJobs = jobs
    },
    SET_RECORDS_JOBSINCOMPANY: (state, jobs) => {
        state.records.jobsInCompany = jobs
    },
    SET_RECORDS_APPLIEDJOBS: (state, jobs, total) => {
        state.records.appliedJobs = jobs;
        state.totalAppliedJobs = total;
    },
    SET_TOTAL_ACTIVEJOBS: (state, total) => {
        state.totalActiveJobs = total
    },
    SET_TOTAL_EXPIREDJOBS: (state, total) => {
        state.totalexpiredJobs = total
    },
    SET_TOTAL_JOBSINCOMPANY: (state, total) => {
        state.totalJobsInCompany = total
    },
    RESET_JOBSINCOMPANY: (state) => {
        state.records.jobsInCompany = [];
        state.totalJobsInCompany = 0;
    },
    UPDATE_RECORDS: (state, param) => {
        let indexActiveJob = state.records.activeJobs.find(obj => obj.id === param.id);
        if (indexActiveJob !== -1) {
            indexActiveJob = param;
        }

        let indexExpiredJobs = state.records.expiredJobs.find(obj => obj.id === param.id);
        if (indexExpiredJobs !== -1) {
            indexExpiredJobs = param;
        }
    },
    SET_SAVED_JOBS: (state, savedJobs, total) => {
        state.records.savedJobs = savedJobs
        state.totalSavedJobs = total
    },
    UPDATE_SAVED_JOBS: (state, job) => {
        if (state.records.savedJobs.includes(job)) {
            state.records.savedJobs.splice(job, 1);
            state.totalSavedJobs--;
        }
        else {
            state.records.savedJobs.unshift(job);
            state.totalSavedJobs++;
        }
    }
}

const actions = {
    // get location
    async getJobs({ commit }, param) {
        return new Promise((resolve, reject) => {
            JobApi.getJobs(param).then(response => {
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
                commit('RESET_JOBSINCOMPANY');
                if (param.companyId) {
                    commit('SET_RECORDS_JOBSINCOMPANY', data.data.items)
                    commit('SET_TOTAL_JOBSINCOMPANY', data.data.totalRecord)
                }
                else {
                    commit('SET_RECORDS_ACTIVEJOBS', data.data.items)
                    commit('SET_TOTAL_ACTIVEJOBS', data.data.totalRecord)
                }
                return resolve(data.data)
            }).catch(error => {
                reject(error)
            })
        })
    },

    async saveJob({ commit }, job) {
        return new Promise((resolve, reject) => {
            JobApi.saveJob(job.id).then(response => {
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

                commit('UPDATE_SAVED_JOBS', job)

                return resolve(data)
            }).catch(error => {
                reject(error)
            })
        })
    },

    async applyJob({ commit }, job) {
        return new Promise((resolve, reject) => {
            JobApi.applyJob(job.id).then(response => {
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
                return resolve(data)
            }).catch(error => {
                reject(error)
            })
        })
    },

    getSavedJobs({ commit }, param) {
        return new Promise((resolve, reject) => {
            JobApi.getSavedJobs(param).then(response => {
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
                commit('SET_SAVED_JOBS', data.data.items, data.data.totalRecord)
                return resolve(data.data)
            }).catch(error => {
                reject(error)
            })
        })
    },

    getAppliedJobs({ commit }, param) {
        return new Promise((resolve, reject) => {
            JobApi.getAppliedJobs(param).then(response => {
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
                commit('SET_RECORDS_APPLIEDJOBS', data.data.items, data.data.totalRecord)
                return resolve(data.data)
            }).catch(error => {
                reject(error)
            })
        })
    },

    getJobBySlug({ commit }, param) {
        return new Promise((resolve, reject) => {
            JobApi.getJobBySlug(param.slug).then(response => {
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
    },

    async create({ commit, dispatch }, param) {

        var data = await JobApi.create(param).then(response => {
            const data = response
            if (!data) {
                throw 'Verification failed, please Login again.'
            }
            if (!data.status) {
                return
            }
            router.push({ name: 'Recruitment' })
            return data
        }).catch(error => {
            throw error
        })
        return data;
    },
    update({ commit }, param) {
        return new Promise((resolve, reject) => {

            JobApi.update(param).then(response => {
                const data = response
                if (!data) {
                    reject('Verification failed, please Login again.')
                }
                if (!data.status) {
                    return resolve()
                }
                commit('UPDATE_RECORDS', param)

                return resolve(data)
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