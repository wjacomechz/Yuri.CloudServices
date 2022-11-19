using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace YURI.CLOUD.TRANSVERSAL.IO.S3.Utilities
{
    public sealed class S3IOFileProvider : IFileProvider
    {
        private readonly string _root;
        private readonly S3IOUtilities s3IOUtilities;

        public S3IOFileProvider(string BucketName, string AccessKey, string SecretKey, string root)
        {
            s3IOUtilities = new S3IOUtilities(BucketName, AccessKey, SecretKey);
            _root = root;
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return null;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            var result = new IOFileInfo(_root, subpath, s3IOUtilities);
            return result.Exists ? result as IFileInfo : new NotFoundFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return new IOChangeToken(filter);
        }

        IChangeToken IFileProvider.Watch(string filter)
        {
            throw new NotImplementedException();
        }

        private sealed class IOFileInfo : IFileInfo
        {
            private string _viewPath;
            private byte[] _viewContent;
            private readonly S3IOUtilities _s3IOUtilities;

            public IOFileInfo(string root, string viewPath, S3IOUtilities s3IOUtilities)
            {
                _viewPath = viewPath;
                _s3IOUtilities = s3IOUtilities;
                GetView(root, viewPath);
            }
            public bool Exists { get; set; }

            public bool IsDirectory => false;

            public DateTimeOffset LastModified { get; set; }

            public long Length
            {
                get
                {
                    using (var stream = new MemoryStream(_viewContent))
                    {
                        return stream.Length;
                    };
                }
            }

            public string Name => Path.GetFileName(_viewPath);
            public string PhysicalPath => null;

            public Stream CreateReadStream()
            {
                return new MemoryStream(_viewContent);
            }

            private void GetView(string root, string viewPath)
            {


                var filePath = root + viewPath;
                try
                {
                    var (Stream, Message) = _s3IOUtilities.DownLoadFileAsync(filePath).Result;
                    {
                        if (Stream == null) throw new Exception(Message);
                        using (var ms = new MemoryStream())
                        {
                            Stream.CopyTo(ms);
                            Stream.Dispose();
                            _viewContent = ms.ToArray();
                            Exists = true;
                        };
                    };
                }
                catch (Exception)
                {
                }
            }
        }

        private sealed class IOChangeToken : IChangeToken
        {
            private string _viewPath;

            public IOChangeToken(string viewPath)
            {
                _viewPath = viewPath;
            }

            public bool ActiveChangeCallbacks => false;

            public bool HasChanged
            {
                get
                {
                    return true;
                }
            }

            public IDisposable RegisterChangeCallback(Action<object> callback, object state) => EmptyDisposable.Instance;
        }

        internal class EmptyDisposable : IDisposable
        {
            public static EmptyDisposable Instance { get; } = new EmptyDisposable();
            private EmptyDisposable() { }
            public void Dispose() { }
        }
    }
}
