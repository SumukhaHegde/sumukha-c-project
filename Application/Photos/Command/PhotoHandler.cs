using Application.Authentication.Interface;
using Application.Photos.Interface;
using Core.Entities;
using FluentResults;
using MediatR;

namespace Application.Photos.Command
{
    public class PhotoHandler : IRequestHandler<PhotoCommand, Result<Photo>>
    {
        private readonly IPhotoAccessor _photoAccessor;
        private readonly ICurrentUserContext _currentUserContext;

        public PhotoHandler(IPhotoAccessor photoAccessor, ICurrentUserContext currentUserContext)
        {
            _photoAccessor = photoAccessor;
            _currentUserContext = currentUserContext;

        }
        public async Task<Result<Photo>> Handle(PhotoCommand request, CancellationToken cancellationToken)
        {
            var userDetails = await _currentUserContext.GetCurrentUser();
            if (userDetails == null)
            {
                return null;
            }

            var photoUploadResult = await _photoAccessor.UploadPhoto(request.file, userDetails.Id);
            var photo = new Photo
            {
                Url = photoUploadResult.Url,
                Id = photoUploadResult.PublicId,
            };

            if (userDetails.Photos.Any(x => x.IsMain)) photo.IsMain = true;

            userDetails.Photos.Add(photo);
            return photo;
        }
    }
}
