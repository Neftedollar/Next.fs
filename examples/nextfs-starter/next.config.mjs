/** @type {import('next').NextConfig} */
const nextConfig = {
  experimental: {
    authInterrupts: true,
    globalNotFound: true,
  },
};

export default nextConfig;
