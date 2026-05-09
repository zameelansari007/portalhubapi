using AutoMapper;
using FluentValidation;
using PortalHub.Application.Common;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces;
using PortalHub.Application.Interfaces.Queries;
using PortalHub.Application.Interfaces.Repositories;
using PortalHub.Domain.Models.Portal;

namespace PortalHub.Application.Services
{
    public class SupplierProfileService :
        ICrudService<
            CreateSupplierProfileDto,
            UpdateSupplierProfileDto,
            SupplierProfileResponseDto>
    {
        private readonly ISupplierProfileQueryRepository _queryRepo;

        private readonly IRepository<SupplierProfile> _commandRepo;

        private readonly IMapper _mapper;

        private readonly IValidator<SupplierProfile> _validator;

        public SupplierProfileService(
            ISupplierProfileQueryRepository queryRepo,
            IRepository<SupplierProfile> commandRepo,
            IMapper mapper,
            IValidator<SupplierProfile> validator)
        {
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ServiceResult<IEnumerable<SupplierProfileResponseDto>>> GetAllAsync()
        {
            var result = await _queryRepo.GetAllAsync();

            return ServiceResult<IEnumerable<SupplierProfileResponseDto>>
                .Ok(result);
        }

        public async Task<ServiceResult<SupplierProfileResponseDto>> GetByIdAsync(object id)
        {
            var result = await _queryRepo.GetByIdAsync((long)id);

            if (result == null)
            {
                return ServiceResult<SupplierProfileResponseDto>
                    .Fail("Supplier profile not found",
                        ErrorCodes.NotFound);
            }

            return ServiceResult<SupplierProfileResponseDto>
                .Ok(result);
        }

        public async Task<ServiceResult<SupplierProfileResponseDto>> CreateAsync(
            CreateSupplierProfileDto dto)
        {
            var entity = _mapper.Map<SupplierProfile>(dto);

            var validation = await _validator.ValidateAsync(entity);

            if (!validation.IsValid)
            {
                return ServiceResult<SupplierProfileResponseDto>
                    .Fail(
                        validation.Errors.First().ErrorMessage,
                        ErrorCodes.ValidationError);
            }

            await _commandRepo.AddAsync(entity);

            await _commandRepo.SaveChangesAsync();

            var result =
                await _queryRepo.GetByIdAsync(entity.SupplierId);

            return ServiceResult<SupplierProfileResponseDto>
                .Ok(result!);
        }

        public async Task<ServiceResult<SupplierProfileResponseDto>> UpdateAsync(
            UpdateSupplierProfileDto dto)
        {
            var entity = _mapper.Map<SupplierProfile>(dto);

            var validation = await _validator.ValidateAsync(entity);

            if (!validation.IsValid)
            {
                return ServiceResult<SupplierProfileResponseDto>
                    .Fail(
                        validation.Errors.First().ErrorMessage,
                        ErrorCodes.ValidationError);
            }

            await _commandRepo.UpdateAsync(entity);

            await _commandRepo.SaveChangesAsync();

            var result =
                await _queryRepo.GetByIdAsync(entity.SupplierId);

            return ServiceResult<SupplierProfileResponseDto>
                .Ok(result!);
        }

        public async Task<ServiceResult<bool>> DeleteAsync(object id)
        {
            var entity =
                await _commandRepo.GetByIdAsync((long)id);

            if (entity == null)
            {
                return ServiceResult<bool>
                    .Fail("Not found", ErrorCodes.NotFound);
            }

            await _commandRepo.DeleteAsync(entity);

            await _commandRepo.SaveChangesAsync();

            return ServiceResult<bool>.Ok(true);
        }
    }
}