using AutoMapper;
using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Application.Service.Base;
using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.Enum;
using Demo.Domain.filter;
using Demo.Domain.IRepository;
using Domain.Infrastructure.Common;
using Domain.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace Demo.Application.Service
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;
        private readonly ITransaction_ServicePackageRepository _transaction_ServicePackageRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IServicePackageRepository _servicePackageRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMediaService _mediaService;
        private readonly IUserRepository _userRepository;
        private readonly FilterHandler _filterHandler;

        public UserService(IMapper map,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration,
            ITransaction_ServicePackageRepository transaction_ServicePackageRepository,
            ITransactionRepository transactionRepository,
            IServicePackageRepository servicePackageRepository,
            IJobRepository jobRepository,
            ICompanyRepository companyRepository,
            IMediaService mediaService,
            IUserRepository userRepository,
            FilterHandler filterHandler) : base(map)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = configuration;
            _transaction_ServicePackageRepository = transaction_ServicePackageRepository;
            _transactionRepository = transactionRepository;
            _servicePackageRepository = servicePackageRepository;
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
            _mediaService = mediaService;
            _userRepository = userRepository;
            _filterHandler = filterHandler;
        }
        public async Task<string> Authenticate(UserModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new Exception("User không tồn tại");
            }
            var res = await _signInManager.PasswordSignInAsync(user, model.Password, model.RemeberMe, false);

            if (!res.Succeeded)
            {
                throw new Exception("Đăng nhập thất bại");
            }
            var roles = await _userManager.GetRolesAsync(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"] ?? ""));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = _config["Tokens:Issuer"];

            var claims = new List<Claim> {
                new Claim(ClaimTypes.GivenName, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));


            var token = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(UserModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                throw new Exception("Email đã tồn tại");
            }
            int? companyId = null;
            if (model.Roles[0] == TypeUser.Company)
            {

                var newCompany = new Company
                {
                    Name = model.CompanyName,
                    Description = model.CompanyDescription,
                    EmployeeCount = model.CompanyEmployeeCount,
                    WebsiteUrl = model.CompanyWebsiteUrl,
                    Address = model.CompanyAddress,
                    ImageId = model.CompanyImageId,
                    PreviewImageId = model.CompanyPreviewImageId,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };
                var slug = GenerateSlug(newCompany.Name);
                newCompany.Slug = slug;
                var i = 1;
                while (await _companyRepository.CheckDuplicateSlugCompany(newCompany.Slug))
                {
                    newCompany.Slug = $"{slug}-{i}";
                    i++;
                }
                if (!(await _companyRepository.CreateAsync(newCompany)))
                {
                    return false;
                }
                companyId = newCompany.Id;
            }
            var user = new User()
            {
                UserName = Guid.NewGuid().ToString(),
                Email = model.Email,
                Dob = model.Dob,
                PhoneNumber = model.PhoneNumber,
                FullName = model.FullName,
                CompanyId = companyId,
                AvatarId = model.AvatarId,
                LocationId = model.LocationId ?? 0,
                Technology = model.Technology,
                FieldId = model.FieldId ?? 0,
            };
            var res = await _userManager.CreateAsync(user, model.Password);

            if (res.Succeeded)
            {
                var assignRole = await AssignRoleToUser(user.Id, model.Roles[0]);
                if (assignRole)
                {
                    var transaction = new Transaction
                    {
                        TransactionDate = DateTime.Now,
                        Fee = 0,
                        Message = "gói miễn phí",
                        Status = TransactionStatus.Success,
                        UserId = user.Id,
                        StartDate = DateTime.Now,
                    };

                    await _transactionRepository.CreateAsync(transaction);

                    if (model.Roles[0] == TypeUser.User)
                    {
                        var userServicePackageId = await _servicePackageRepository.GetIdByNameSystem("FreeUser");
                        var tSP = new Transaction_ServicePackage
                        {
                            TransactionId = transaction.Id,
                            ServicePackageId = userServicePackageId
                        };

                        await _transaction_ServicePackageRepository.CreateAsync(tSP);
                    }
                    else if (model.Roles[0] == TypeUser.Company)
                    {
                        var companyServicePackageId = await _servicePackageRepository.GetIdByNameSystem("FreeCompany");
                        var tSP = new Transaction_ServicePackage
                        {
                            TransactionId = transaction.Id,
                            ServicePackageId = companyServicePackageId
                        };

                        await _transaction_ServicePackageRepository.CreateAsync(tSP);
                    }
                    return true;
                }
            }
            throw new Exception("Lỗi đăng ký");
        }

        public async Task<List<string>?> GetRolesAsync(UserModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var roles = (List<string>)await _userManager.GetRolesAsync(user);

            return roles;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new Exception("User không tồn tại");
            }
            if (user.CompanyId != null)
            {
                var company = await _companyRepository.GetByIdAsync(user.CompanyId ?? 0);
                await _companyRepository.DeleteAsync(company);
            }
            user.Deleted = true;
            var res = await _userManager.UpdateAsync(user);
            if (res.Succeeded)
                return true;
            throw new Exception("Xóa thất bại");
        }

        public async Task<bool> Update(int id, UserModel model)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {

                var company = await _companyRepository.GetByIdAsync(model.CompanyId ?? 0);
                //update company
                if (company != null)
                {
                    company.Name = model.CompanyName;
                    company.Description = model.CompanyDescription;
                    company.EmployeeCount = model.CompanyEmployeeCount;
                    company.WebsiteUrl = model.CompanyWebsiteUrl;
                    company.Address = model.CompanyAddress;
                    company.ImageId = model.CompanyImageId ?? company.ImageId;
                    company.PreviewImageId = model.CompanyPreviewImageId ?? company.PreviewImageId;
                    company.UpdateAt = DateTime.Now;

                    var slug = GenerateSlug(company.Name);
                    company.Slug = slug;
                    var i = 1;
                    while (await _companyRepository.CheckDuplicateSlugCompany(company.Slug))
                    {
                        company.Slug = $"{slug}-{i}";
                        i++;
                    }
                    await _companyRepository.UpdateAsync(company);
                }

                //update user
                if (user.Email != model.Email)
                {
                    if (await _userManager.Users.AnyAsync(x => x.Email == model.Email && x.Id != id))
                    {
                        throw new Exception("Email đã tồn tại");
                    }
                }
                user.FullName = model.FullName;
                user.FileCVId = model.FileCVId;
                user.Dob = model.Dob;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.AvatarId = model.AvatarId ?? user.AvatarId;
                user.Technology = model.Technology;
                user.FieldId = model.FieldId ?? 0;
                user.LocationId = model.LocationId ?? 0;
                var res = await _userManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    return true;
                }
            }
            throw new Exception("Cập nhật thất bại");
        }

        public async Task<bool?> SaveJobAsync(Job_UserModel model)
        {
            var jobUser = await _jobRepository.GetByJobUserId(model.JobId, model.UserId);
            if (jobUser == null)
            {
                var newJobUser = _mapper.Map<Job_User>(model);
                newJobUser!.CreateAt = DateTime.Now;
                return await _jobRepository.CreateJobUser(newJobUser);
            }
            var newStatefollowedJob = !jobUser.FollowedJob;
            jobUser.FollowedJob = newStatefollowedJob;
            var isSuccessed = await _jobRepository.UpdateJobUserAsync(jobUser);
            if (isSuccessed)
            {
                return jobUser.FollowedJob;
            }
            return null;
        }

        public async Task<bool> UpdateJobUser(Job_UserModel model)
        {
            var jobUser = await _jobRepository.GetByJobUserId(model.JobId, model.UserId);
            if (jobUser == null)
            {
                return false;
            }

            jobUser.Viewed = model.Viewed;
            jobUser.Interested = model.Interested;

            var isSuccessed = await _jobRepository.UpdateJobUserAsync(jobUser);

            return isSuccessed;
        }

        public async Task<bool> ApplyJob(Job_UserModel model)
        {
            var user = await _userManager.Users.Where(x => x.Id == model.UserId).FirstOrDefaultAsync();
            if (user.FileCVId == null)
            {
                throw new Exception("Cần tải lên cv trước khi ứng tuyển");
            }
            var jobUser = await _jobRepository.GetByJobUserId(model.JobId, model.UserId);
            if (jobUser == null)
            {
                var newJobUser = _mapper.Map<Job_User>(model);
                newJobUser!.CreateAt = DateTime.Now;
                newJobUser!.UpdateAt = DateTime.Now;
                return await _jobRepository.CreateJobUser(newJobUser);
            }
            jobUser.Applied = true;
            jobUser.UpdateAt = DateTime.Now;
            var isSuccessed = await _jobRepository.UpdateJobUserAsync(jobUser);
            return isSuccessed;
        }
        public async Task<DataObject<UserModel>> GetPagingAsync(string roleName, FilterUser? filter)
        {
            var users = (await _userManager
                .GetUsersInRoleAsync(roleName))
                .Where(x => !x.Deleted &&
                            (filter.UserId == null || x.Id == filter.UserId) &&
                            (string.IsNullOrEmpty(filter.KeyWord) || x.FullName.Contains(filter.KeyWord))
                            );

            var total = users.Count();

            var userPagination = filter.PageIndex != 0 ? users
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize) : users;

            var userModels = _mapper.Map<List<UserModel>>(userPagination);
            foreach (var userModel in userModels)
            {
                userModel.Roles = new List<string> { roleName };
                var company = await _companyRepository.GetByIdAsync(userModel.CompanyId ?? 0);
                userModel.AvatarUrl = await _mediaService.GetUrlAsync(userModel.AvatarId ?? 0);
                if (company != null)
                {
                    userModel.CompanyName = company.Name;
                    userModel.CompanyDescription = company.Description;
                    userModel.CompanyEmployeeCount = company.EmployeeCount;
                    userModel.CompanyWebsiteUrl = company.WebsiteUrl;
                    userModel.CompanyAddress = company.Address;
                    userModel.PreviewImgUrl = await _mediaService.GetUrlAsync(company.PreviewImageId ?? 0);
                    userModel.ImgUrl = await _mediaService.GetUrlAsync(company.ImageId ?? 0);
                }
            }
            return new DataObject<UserModel>(userModels, total);
        }

        public async Task<DataObject<JobModel>> getSavedJobsPagingAsync(int id, FilterJob filter)
        {
            var jobIds = await _jobRepository.getSavedJobIdsAsync(id);
            filter.JobIds = jobIds;
            filter.Published = true;

            var jobs = await _jobRepository.GetPagingAsync(filter);
            var jobsModel = _mapper.Map<List<JobModel>>(jobs.Items);
            foreach (var job in jobsModel)
            {
                job.CompanyPreviewImgUrl = await _mediaService.GetUrlAsync(job.CompanyPreviewImageId ?? 0);
                job.CompanyImgUrl = await _mediaService.GetUrlAsync(job.CompanyImageId ?? 0);
            }

            return new DataObject<JobModel>(jobsModel, jobs.TotalRecord);
        }

        public async Task<DataObject<JobModel>> getAppliedJobsPagingAsync(int id, FilterJob filter)
        {
            var jobIds = await _jobRepository.getAppliedJobIdsAsync(id);
            filter.JobIds = jobIds;
            filter.Published = true;

            var jobs = await _jobRepository.GetPagingAsync(filter);
            var jobsModel = _mapper.Map<List<JobModel>>(jobs.Items);
            foreach (var job in jobsModel)
            {
                job.CompanyPreviewImgUrl = await _mediaService.GetUrlAsync(job.CompanyPreviewImageId ?? 0);
                job.CompanyImgUrl = await _mediaService.GetUrlAsync(job.CompanyImageId ?? 0);
            }

            return new DataObject<JobModel>(jobsModel, jobs.TotalRecord);
        }

        public async Task<DataObject<UserModel>> GetCandidatesAppliedPaging(FilterCandidate filter)
        {
            var data = await _userRepository.GetCandidatesAppliedPagingAsync(filter);
            var userModels = new List<UserModel>();
            foreach (var jobUser in data.Items)
            {
                var userModel = _mapper.Map<UserModel>(jobUser.User);
                userModel.CreateAt = jobUser.CreateAt;
                userModel.UpdateAt = jobUser.UpdateAt;
                userModel.AvatarUrl = await _mediaService.GetUrlAsync(userModel.AvatarId ?? 0);
                userModel.FileCVUrl = await _mediaService.GetUrlAsync(userModel.FileCVId ?? 0);
                userModels.Add(userModel);
            }
            return new DataObject<UserModel>(userModels, data.TotalRecord);
        }

        public async Task<DataObject<UserModel>> GetCandidatesPaging(FilterCandidate filter)
        {
            var users = (await _userManager
            .GetUsersInRoleAsync("user"))
            .OrderByDescending(x => x.Id)
            .Where(x => !x.Deleted &&
                        x.FileCVId != null &&
                         (string.IsNullOrEmpty(filter.KeyWord) || (x.Technology?.ToLower().Contains(filter.KeyWord.ToLower()) ?? false)));
            users = _filterHandler.HandleFilterCandidate(users, filter);
            var total = users?.Count() ?? 0;
            var userPagination = filter.PageIndex != 0 ? users
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize) : users;
            var userModels = _mapper.Map<List<UserModel>>(userPagination);
            foreach (var userModel in userModels)
            {
                userModel.AvatarUrl = await _mediaService.GetUrlAsync(userModel.AvatarId ?? 0);
                userModel.FileCVUrl = await _mediaService.GetUrlAsync(userModel.FileCVId ?? 0);
            }

            return new DataObject<UserModel>(userModels, total);
        }

        public async Task<UserModel?> GetUserByIdAsync(int id)
        {
            var users = _userManager.Users;
            var user = users.AsNoTracking().Where(x => !x.Deleted && x.Id == id).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("admin"))
            {
                var usermodel = _mapper.Map<UserModel>(user);
                usermodel.AvatarUrl = await _mediaService.GetUrlAsync(user.AvatarId ?? 0);
                usermodel.Roles = role.ToList();
                return usermodel;
            }
            return new UserModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Dob = user.Dob,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = role.ToList(),
                AvatarId = user.AvatarId,
                AvatarUrl = await _mediaService.GetUrlAsync(user.AvatarId ?? 0),
                FileCVId = user.FileCVId,
                FileCVUrl = role.Contains("user") ? await _mediaService.GetUrlAsync(user.FileCVId ?? 0) : null,
                LocationId = user.LocationId,
                FieldId = user.FieldId,
                CompanyId = user.CompanyId,
                Technology = user.Technology
            };
        }

        public async Task<bool> AssignRoleToUser(int id, string role)
        {
            var query = await _roleManager.FindByNameAsync(role);
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (query == null)
            {
                await _userManager.DeleteAsync(user);
                throw new Exception("Lỗi role");
            }
            var oldRoleNames = await _userManager.GetRolesAsync(user ?? new User());
            if (oldRoleNames.Contains(role))
            {
                throw new Exception("Trùng role");
            }
            var res = await _userManager.AddToRoleAsync(user, role);
            if (!res.Succeeded)
            {
                throw new Exception("Thêm role thất bại");
            }
            return true;
        }


    }
}
