using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Maui.Graphics
{
    public class VirtualImage : IImage
    {
        private readonly byte[] _bytes;
        private readonly int _width;
        private readonly int _height;
        private readonly ImageFormat _originalFormat;

        public VirtualImage(byte[] bytes, ImageFormat originalFormat = ImageFormat.Png)
        {
            _bytes = bytes;
            _originalFormat = originalFormat;

            if (originalFormat == ImageFormat.Jpeg)
            {
                GetJpegDimension(out _width, out _height);
            }
            else
            {
                GetPngDimension(out _width, out _height);
            }
        }

        public byte[] Bytes => _bytes;

        public IImage Downsize(float maxWidthOrHeight, bool disposeOriginal = false)
        {
#if DEBUG
            Logger.Debug("Downsizing not supported in virtual image.");
#endif
            return this;
        }

        public IImage Downsize(float maxWidth, float maxHeight, bool disposeOriginal = false)
        {
#if DEBUG
            Logger.Debug("Downsizing not supported in virtual image.");
#endif
            return this;
        }

        public IImage Resize(float width, float height, ResizeMode resizeMode = ResizeMode.Fit, bool disposeOriginal = false)
        {
#if DEBUG
            Logger.Debug("Resizing not supported in virtual image.");
#endif
            return this;
        }
        
        public void Save(Stream stream, ImageFormat format = ImageFormat.Png, float quality = 1)
        {
            if (format == _originalFormat)
            {
                stream.Write(_bytes, 0, _bytes.Length);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public Task SaveAsync(Stream stream, ImageFormat format = ImageFormat.Png, float quality = 1)
        {
            if (format == _originalFormat)
            {
                Save(stream, format, quality);
                return Task.FromResult(true);
            }

            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // Do nothing
        }

        public float Width => _width;

        public float Height => _height;

        private void GetPngDimension(out int width, out int height)
        {
            width = 0;
            height = 0;

            // Look into the byte array and get the size of the image
            for (int i = 0; i <= 3; i++)
            {
                width = _bytes[i] | width << 8;
                height = _bytes[i + 4] | height << 8;
            }
        }

        private void GetJpegDimension(out int width, out int height)
        {
            width = 0;
            height = 0;

            bool found = false;
            bool eof = false;

            var stream = new MemoryStream(_bytes);
            var reader = new BinaryReader(stream);

            while (!found || eof)
            {
                // read 0xFF and the type
                reader.ReadByte();
                byte type = reader.ReadByte();

                // get length
                int len;
                switch (type)
                {
                    // start and end of the image
                    case 0xD8:
                    case 0xD9:
                        len = 0;
                        break;

                    // restart interval
                    case 0xDD:
                        len = 2;
                        break;

                    // the next two bytes is the length
                    default:
                        int lenHi = reader.ReadByte();
                        int lenLo = reader.ReadByte();
                        len = (lenHi << 8 | lenLo) - 2;
                        break;
                }

                // EOF?
                if (type == 0xD9)
                    eof = true;

                // process the data
                if (len > 0)
                {
                    // read the data
                    byte[] data = reader.ReadBytes(len);

                    // this is what we are looking for
                    if (type == 0xC0)
                    {
                        width = data[1] << 8 | data[2];
                        height = data[3] << 8 | data[4];
                        found = true;
                    }
                }
            }

            reader.Dispose();
            stream.Dispose();
        }

        public void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            throw new Exception("Drawing a virtual image is not supported.");
        }
    }
}