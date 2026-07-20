import secrets
import base64

def generate_aes_key():
    key_bytes = secrets.token_bytes(32)
    key = base64.b64encode(key_bytes).decode('utf-8')

    print(f'The AES-256 key is generated: {key}')

if __name__ == '__main__':
    generate_aes_key()
