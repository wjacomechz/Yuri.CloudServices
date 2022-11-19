using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace YURI.CLOUD.TRANSVERSAL.IO.S3.Utilities
{
    internal sealed class S3IOUtilities
    {
        private readonly string BucketName;
        private readonly string AccessKey;
        private readonly string SecretKey;
        private readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;

        internal S3IOUtilities(string BucketName, string AccessKey, string SecretKey)
        {
            this.BucketName = BucketName;
            this.AccessKey = AccessKey;
            this.SecretKey = SecretKey;
        }

        internal async Task<(Stream Stream, string Message)> DownLoadFileAsync(string key)
        {
            try
            {
                AmazonS3Config config = new AmazonS3Config() { RegionEndpoint = bucketRegion };
                AmazonS3Client s3Client = new AmazonS3Client(AccessKey, SecretKey, config);
                using (var transferUtility = new TransferUtility(s3Client))
                {
                    var stm = await transferUtility.OpenStreamAsync(BucketName, key);
                    return (stm, null);
                };
            }
            catch (AmazonS3Exception ex)
            {
                return (null, string.Format("AmazonS3Exception: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                return (null, string.Format("Exception: {0}", ex.Message));
            }
        }

        internal async Task<(List<Stream> Stream, string Message)> DownLoadFilexPrefixAsync(string prefix)
        {
            try
            {
                var config = new AmazonS3Config() { RegionEndpoint = bucketRegion };
                var s3Client = new AmazonS3Client(AccessKey, SecretKey, config);
                var request = new ListObjectsV2Request
                {
                    BucketName = BucketName,
                    Prefix = prefix,
                    //Delimiter = "/",
                    MaxKeys = 1000
                };
                var response = await s3Client.ListObjectsV2Async(request);
                var x = response.S3Objects;
                if (x?.Count == 0) throw new AmazonS3Exception("Archivo no encontrado");
                var lst = new List<Stream>();
                foreach (var objt in x)
                {
                    if (objt.Size > 0)
                    {
                        var request1 = new GetObjectRequest
                        {
                            BucketName = BucketName,
                            Key = objt.Key
                        };
                        var Response = await s3Client.GetObjectAsync(request1);
                        using (Stream responseStream = Response.ResponseStream)
                        {
                            lst.Add(responseStream);
                        }
                    }
                }
                return (lst, null);
            }
            catch (AmazonS3Exception ex)
            {
                return (null, string.Format("AmazonS3Exception: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                return (null, string.Format("Exception: {0}", ex.Message));
            }
        }

        internal async Task<(bool IsSucceed, string Message)> UpLoadFolderAsync(string folderPath)
        {
            folderPath += "/"; //end the folder name with "/"
            return await UpLoad(folderPath, null);
        }

        internal async Task<(bool IsSucceed, string Message)> UpLoadFileAsync(string filePath, MemoryStream file)
        {
            return await UpLoad(filePath, file);
        }

        internal async Task<(bool IsSucceed, string Message)> UpLoad(string key, MemoryStream file)
        {
            try
            {
                AmazonS3Config config = new AmazonS3Config() { RegionEndpoint = bucketRegion };
                using (AmazonS3Client s3Client = new AmazonS3Client(AccessKey, SecretKey, config))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = BucketName,
                        StorageClass = S3StorageClass.Standard,
                        ServerSideEncryptionMethod = ServerSideEncryptionMethod.None,
                        Key = key,
                    };
                    if (file != null)
                    {
                        request.InputStream = file;
                        request.CannedACL = S3CannedACL.Private;
                    }
                    else
                        request.ContentBody = string.Empty;
                    await s3Client.PutObjectAsync(request);
                    return (true, null);
                };
            }
            catch (AmazonS3Exception ex)
            {
                return (false, string.Format("AmazonS3Exception: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                return (false, string.Format("Exception: {0}", ex.Message));
            }
        }

        internal async Task<(bool IsSucceed, string Message)> DeleteFileAsync(string key)
        {
            try
            {
                AmazonS3Config config = new AmazonS3Config() { RegionEndpoint = bucketRegion };
                using (AmazonS3Client s3Client = new AmazonS3Client(AccessKey, SecretKey, config))
                {
                    var deleteObjectRequest = new DeleteObjectRequest
                    {
                        BucketName = BucketName,
                        Key = key
                    };
                    await s3Client.DeleteObjectAsync(deleteObjectRequest);
                    return (true, null);
                };
            }
            catch (AmazonS3Exception ex)
            {
                return (false, string.Format("AmazonS3Exception: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                return (false, string.Format("Exception: {0}", ex.Message));
            }
        }

        internal async Task<(bool IsSucceed, string Message)> FileExistsAsync(string key)
        {
            try
            {
                AmazonS3Config config = new AmazonS3Config() { RegionEndpoint = bucketRegion };
                using (AmazonS3Client s3Client = new AmazonS3Client(AccessKey, SecretKey, config))
                {
                    var request = new ListObjectsRequest
                    {
                        BucketName = BucketName,
                        Marker = key,
                        MaxKeys = 1
                    };

                    var response = await s3Client.ListObjectsAsync(request, CancellationToken.None);
                    if (response.S3Objects.Any()) return (true, null);
                    return (false, "Archivo no encontrado");
                };
            }
            catch (AmazonS3Exception ex)
            {
                return (false, string.Format("AmazonS3Exception: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                return (false, string.Format("Exception: {0}", ex.Message));
            }
        }
    }
}
