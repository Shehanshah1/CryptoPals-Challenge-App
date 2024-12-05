#include "tester.h"

// The code that is run and returns the compare value.
















/*




//Challenge 8 

#include <iostream>
#include <fstream>
#include <vector>
#include <set>
#include <string>

using namespace std;

// Function to convert a hex string to a vector of bytes
vector<uint8_t> hexToBytes(const string& hex) {
    vector<uint8_t> bytes;
    for (size_t i = 0; i < hex.length(); i += 2) {
        string byteString = hex.substr(i, 2);
        uint8_t byte = static_cast<uint8_t>(stoi(byteString, nullptr, 16));
        bytes.push_back(byte);
    }
    return bytes;
}

// Function to detect if a ciphertext has been encrypted using ECB mode
bool hasRepeatedBlocks(const vector<uint8_t>& ciphertext, size_t blockSize) {
    set<vector<uint8_t>> uniqueBlocks;
    for (size_t i = 0; i < ciphertext.size(); i += blockSize) {
        vector<uint8_t> block(ciphertext.begin() + i, ciphertext.begin() + i + blockSize);
        if (uniqueBlocks.find(block) != uniqueBlocks.end()) {
            return true; // Repeated block found
        }
        uniqueBlocks.insert(block);
    }
    return false;
}

string tester() {
    ifstream file("C:/VSProjects/Sample-Test1/MyTestSuite/Chall8.txt");
    vector<string> ciphertexts;
    string line;

    // Read ciphertexts from the file
    if (file.is_open()) {
        while (getline(file, line)) {
            if (!line.empty()) {
                ciphertexts.push_back(line);
            }
        }
        file.close();
    }
    else {
        cerr << "Unable to open file" << endl;
        return "Fail";
    }

    size_t blockSize = 16; // AES block size (16 bytes)

    // Detect ECB-encrypted ciphertext
    for (size_t i = 0; i < ciphertexts.size(); ++i) {
        vector<uint8_t> ciphertextBytes = hexToBytes(ciphertexts[i]);
        if (hasRepeatedBlocks(ciphertextBytes, blockSize)) {
            cout << "ECB-encrypted ciphertext found at index " << i << endl;
            cout << "Ciphertext: " << ciphertexts[i] << endl;
        }
    }

    return ciphertexts[0];
}



*/


//Challenge 6

//Challenge 7 

/*

#include <iostream>
#include <vector>
#include <string>
#include <cstring>
#include <fstream>
#include <sstream>
#include <cstdint>

// AES S-box
const unsigned char sbox[256] = {
    0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76,
       0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0,
       0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15,
       0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75,
       0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84,
       0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf,
       0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8,
       0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2,
       0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73,
       0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb,
       0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79,
       0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08,
       0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a,
       0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e,
       0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf,
       0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16 };

// AES inverse S-box
const unsigned char inv_sbox[256] = {
    0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb,
       0x7c, 0xe3, 0x39, 0x82, 0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb,
       0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d, 0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e,
       0x08, 0x2e, 0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25,
       0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92,
       0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15, 0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84,
       0x90, 0xd8, 0xab, 0x00, 0x8c, 0xbc, 0xd3, 0x0a, 0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3, 0x45, 0x06,
       0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02, 0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b,
       0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea, 0x97, 0xf2, 0xcf, 0xce, 0xf0, 0xb4, 0xe6, 0x73,
       0x96, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e,
       0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7, 0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b,
       0xfc, 0x56, 0x3e, 0x4b, 0xc6, 0xd2, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd, 0x5a, 0xf4,
       0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31, 0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f,
       0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d, 0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef,
       0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0, 0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61,
       0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0c, 0x7d };

// AES round constant
const unsigned char Rcon[11] = {
    0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36
};

const int Nk = 4; // Number of 32-bit words in the key (128 bits / 32 bits)
const int Nb = 4; // Number of columns (32-bit words) in the state
const int Nr = 10; // Number of rounds for AES-128
// Key Expansion
void KeyExpansion(unsigned char* key, unsigned char* expandedKey) {
    int i = 0;
    while (i < Nk) {
        expandedKey[i * 4 + 0] = key[i * 4 + 0];
        expandedKey[i * 4 + 1] = key[i * 4 + 1];
        expandedKey[i * 4 + 2] = key[i * 4 + 2];
        expandedKey[i * 4 + 3] = key[i * 4 + 3];
        i++;
    }

    // Generate round keys
    for (i = Nk; i < Nb * (Nr + 1); i++) {
        unsigned char temp[4];
        temp[0] = expandedKey[(i - 1) * 4 + 0];
        temp[1] = expandedKey[(i - 1) * 4 + 1];
        temp[2] = expandedKey[(i - 1) * 4 + 2];
        temp[3] = expandedKey[(i - 1) * 4 + 3];

        if (i % Nk == 0) {
            // Rotate
            std::rotate(temp, temp + 1, temp + 4);
            // Apply S-box
            for (int j = 0; j < 4; j++) {
                temp[j] = sbox[temp[j]];
            }
            // XOR with Rcon
            temp[0] ^= Rcon[i / Nk];
        }

        expandedKey[i * 4 + 0] = expandedKey[(i - Nk) * 4 + 0] ^ temp[0];
        expandedKey[i * 4 + 1] = expandedKey[(i - Nk) * 4 + 1] ^ temp[1];
        expandedKey[i * 4 + 2] = expandedKey[(i - Nk) * 4 + 2] ^ temp[2];
        expandedKey[i * 4 + 3] = expandedKey[(i - Nk) * 4 + 3] ^ temp[3];
    }
}

// Add Round Key
void AddRoundKey(unsigned char* state, unsigned char* roundKey) {
    for (int i = 0; i < 16; i++) {
        state[i] ^= roundKey[i];
    }
}

// Inverse Sub Bytes
void InvSubBytes(unsigned char* state) {
    for (int i = 0; i < 16; i++) {
        state[i] = inv_sbox[state[i]];
    }
}

// Inverse Shift Rows
void InvShiftRows(unsigned char* state) {
    unsigned char temp;
    // Row 1
    temp = state[13];
    state[13] = state[9];
    state[9] = state[5];
    state[5] = state[1];
    state[1] = temp;
    // Row 2
    temp = state[14];
    state[14] = state[6];
    state[6] = temp;
    temp = state[10];
    state[10] = state[2];
    state[2] = temp;
    // Row 3
    temp = state[3];
    state[3] = state[7];
    state[7] = state[11];
    state[11] = state[15];
    state[15] = temp;
}

uint8_t gmul(uint8_t a, uint8_t b) {
    uint8_t p = 0;
    for (int i = 0; i < 8; i++) {
        if (b & 1)
            p ^= a;
        bool hi_bit_set = (a & 0x80);
        a <<= 1;
        if (hi_bit_set)
            a ^= 0x1B; // x^8 + x^4 + x^3 + x + 1
        b >>= 1;
    }
    return p;
}
// Inverse Mix Columns
void InvMixColumns(unsigned char* state) {
    for (int i = 0; i < 4; i++) {
        unsigned char a = state[i * 4 + 0];
        unsigned char b = state[i * 4 + 1];
        unsigned char c = state[i * 4 + 2];
        unsigned char d = state[i * 4 + 3];

        state[i * 4 + 0] = gmul(a, 14) ^ gmul(b, 11) ^ gmul(c, 13) ^ gmul(d, 9);
        state[i * 4 + 1] = gmul(a, 9) ^ gmul(b, 14) ^ gmul(c, 11) ^ gmul(d, 13);
        state[i * 4 + 2] = gmul(a, 13) ^ gmul(b, 9) ^ gmul(c, 14) ^ gmul(d, 11);
        state[i * 4 + 3] = gmul(a, 11) ^ gmul(b, 13) ^ gmul(c, 9) ^ gmul(d, 14);
    }
}

// AES Decryption
void AESDecrypt(unsigned char* input, unsigned char* output, unsigned char* expandedKey, int numberOfRounds) {
    unsigned char state[16];
    memcpy(state, input, 16);

    AddRoundKey(state, expandedKey + 16 * numberOfRounds);

    for (int i = numberOfRounds - 1; i > 0; i--) {
        InvShiftRows(state);
        InvSubBytes(state);
        AddRoundKey(state, expandedKey + 16 * i);
        InvMixColumns(state);
    }

    InvShiftRows(state);
    InvSubBytes(state);
    AddRoundKey(state, expandedKey);

    memcpy(output, state, 16);
}

// Base64 decoding
std::vector<unsigned char> base64Decode(const std::string& input) {
    const std::string base64_chars =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        "abcdefghijklmnopqrstuvwxyz"
        "0123456789+/";

    std::vector<unsigned char> decoded;
    int val = 0, valb = -8;
    for (unsigned char c : input) {
        if (c == '=') break;
        size_t pos = base64_chars.find(c);
        if (pos == std::string::npos) continue; // Skip any characters not in base64_chars
        val = (val << 6) + static_cast<int>(pos);
        valb += 6;
        if (valb >= 0) {
            decoded.push_back(static_cast<unsigned char>((val >> valb) & 0xFF));
            valb -= 8;
        }
    }
    return decoded;
}

// Read file content
std::string readFile(const std::string& filename) {
    std::ifstream file(filename);
    if (!file.is_open()) {
        throw std::runtime_error("Unable to open file: " + filename);
    }

    std::stringstream buffer;
    buffer << file.rdbuf();
    return buffer.str();
}

string tester() {
    std::string filename = "C:/VSProjects/Sample-Test1/MyTestSuite/Chall7.txt";
    std::string base64Input;

    try {
        base64Input = readFile(filename);
    }
    catch (const std::exception& e) {
        std::cerr << "Error: " << e.what() << std::endl;
        return "fail";
    }

    std::vector<unsigned char> ciphertext = base64Decode(base64Input);

    unsigned char key[17] = "YELLOW SUBMARINE";
    unsigned char expandedKey[176];
    KeyExpansion(key, expandedKey);

    // Prepare output vector for plaintext
    std::vector<unsigned char> plaintext(ciphertext.size());

    // Decrypt each block (16 bytes)
    for (size_t i = 0; i < ciphertext.size(); i += 16) {
        unsigned char block[16];
        size_t blockSize = std::min(size_t(16), ciphertext.size() - i);
        memcpy(block, &ciphertext[i], blockSize);

        unsigned char decryptedBlock[16];
        AESDecrypt(block, decryptedBlock, expandedKey, Nr);

        memcpy(&plaintext[i], decryptedBlock, blockSize);
    }

    // Convert plaintext to string
    std::string decryptedText(plaintext.begin(), plaintext.end());

    // Optionally print the decrypted text
    std::cout << "Decrypted text: " << decryptedText << std::endl;

    return decryptedText;
}

 */

/*

//Challenge 5

#include <iostream>
#include <string>
#include <vector>
#include <iomanip>
#include <sstream>

using namespace std;

// Convert a string to a vector of bytes
vector<uint8_t> string_to_bytes(const string& str) {
    return vector<uint8_t>(str.begin(), str.end());
}

// XOR the plaintext with the repeating key
vector<uint8_t> repeating_key_xor(const vector<uint8_t>& plaintext, const vector<uint8_t>& key) {
    vector<uint8_t> result;
    size_t key_len = key.size();

    for (size_t i = 0; i < plaintext.size(); ++i) {
        // XOR the i-th byte of plaintext with the corresponding key byte
        result.push_back(plaintext[i] ^ key[i % key_len]);
    }

    return result;
}

// Convert a vector of bytes to a hex string
string bytes_to_hex(const vector<uint8_t>& bytes) {
    stringstream hex_stream;
    for (auto byte : bytes) {
        hex_stream << hex << setw(2) << setfill('0') << (int)byte;
    }
    return hex_stream.str();
}

string tester() {
    // Define the plaintext and the key
    string plaintext = "Burning 'em, if you ain't quick and nimble\nI go crazy when I hear a cymbal";
    string key = "ICE";

    // Convert the plaintext and key to byte vectors
    vector<uint8_t> plaintext_bytes = string_to_bytes(plaintext);
    vector<uint8_t> key_bytes = string_to_bytes(key);

    // Encrypt the plaintext using the repeating-key XOR
    vector<uint8_t> encrypted = repeating_key_xor(plaintext_bytes, key_bytes);

    // Convert the encrypted result to a hex string and print it
    string encrypted_hex = bytes_to_hex(encrypted);
    cout << "Encrypted (hex): " << encrypted_hex << endl;

    return encrypted_hex;
}


*/




/*

#include <iostream>
#include <string>
#include <sstream>
#include <vector>
#include <cctype>
#include <map>
#include <iomanip>
#include <fstream>

using namespace std;

// Frequency of letters in English (normalized values)
map<char, double> english_frequencies = {
    {'a', 0.0651738}, {'b', 0.0124248}, {'c', 0.0217339}, {'d', 0.0349835},
    {'e', 0.1041442}, {'f', 0.0197881}, {'g', 0.0158610}, {'h', 0.0492888},
    {'i', 0.0558094}, {'j', 0.0009033}, {'k', 0.0050529}, {'l', 0.0331490},
    {'m', 0.0202124}, {'n', 0.0564513}, {'o', 0.0596302}, {'p', 0.0137645},
    {'q', 0.0008606}, {'r', 0.0497563}, {'s', 0.0515760}, {'t', 0.0729357},
    {'u', 0.0225134}, {'v', 0.0082903}, {'w', 0.0171272}, {'x', 0.0013692},
    {'y', 0.0145984}, {'z', 0.0007836}, {' ', 0.1918182}
};

// Convert a hex string to a vector of bytes
vector<uint8_t> hex_to_bytes(const string& hex) {
    vector<uint8_t> bytes;
    for (size_t i = 0; i < hex.length(); i += 2) {
        string byte_string = hex.substr(i, 2);
        uint8_t byte = (uint8_t)strtol(byte_string.c_str(), nullptr, 16);
        bytes.push_back(byte);
    }
    return bytes;
}

// XOR the byte vector with a single character key
string xor_with_key(const vector<uint8_t>& bytes, char key) {
    string result;
    for (auto byte : bytes) {
        result += (char)(byte ^ key);
    }
    return result;
}

// Score the plaintext based on English letter frequencies
double score_text(const std::string& text) {
    double score = 0;
    for (char c : text) {
        c = tolower(c);
        if (english_frequencies.find(c) != english_frequencies.end()) {
            score += english_frequencies[c];
        }
    }
    return score;
}

string tester() {
    ifstream file("C:/VSProjects/Sample-Test1/MyTestSuite/Test.txt");
    string hex_input;
    double overall_best_score = -1;
    string overall_best_plaintext;
    char overall_best_key = 'a';
    string best_hex;

    // Read each line from the file
    while (getline(file, hex_input)) {
        vector<uint8_t> bytes = hex_to_bytes(hex_input);

        double best_score = -1;
        string best_plaintext;
        char best_key;

        // Try every possible single character (byte) as the XOR key
        for (int key = 0; key < 256; ++key) {
            string decrypted = xor_with_key(bytes, (char)key);
            double score = score_text(decrypted);

            // Keep track of the highest-scoring (best) result for the current string
            if (score > best_score) {
                best_score = score;
                best_plaintext = decrypted;
                best_key = key;
            }
        }

        // Keep track of the highest-scoring (best) result across all strings
        if (best_score > overall_best_score) {
            overall_best_score = best_score;
            overall_best_plaintext = best_plaintext;
            overall_best_key = best_key;
            best_hex = hex_input;
        }
    }

    // Output the best result
    cout << "Best key: " << hex << setw(2) << setfill('0') << overall_best_key << endl;
    cout << "Hex input: " << best_hex << endl;
    cout << "Decrypted message: " << overall_best_plaintext << endl;

    return overall_best_plaintext;
}

*/







/*

//Challenge 3


#include <iostream>
#include <string>
#include <sstream>
#include <vector>
#include <cctype>
#include <map>
#include <iomanip>

using namespace std;

//**********************************************************************************
//**********************************************************************************
//               Frequency of letters in English (normalized values)
//**********************************************************************************
//**********************************************************************************
map<char, double> english_frequencies = {
    {'a', 0.0651738}, {'b', 0.0124248}, {'c', 0.0217339}, {'d', 0.0349835},
    {'e', 0.1041442}, {'f', 0.0197881}, {'g', 0.0158610}, {'h', 0.0492888},
    {'i', 0.0558094}, {'j', 0.0009033}, {'k', 0.0050529}, {'l', 0.0331490},
    {'m', 0.0202124}, {'n', 0.0564513}, {'o', 0.0596302}, {'p', 0.0137645},
    {'q', 0.0008606}, {'r', 0.0497563}, {'s', 0.0515760}, {'t', 0.0729357},
    {'u', 0.0225134}, {'v', 0.0082903}, {'w', 0.0171272}, {'x', 0.0013692},
    {'y', 0.0145984}, {'z', 0.0007836}, {' ', 0.1918182}
};
//**********************************************************************************
//**********************************************************************************
//                  Convert a hex string to a vector of bytes
//**********************************************************************************
//**********************************************************************************
vector<uint8_t> hex_to_bytes(const string& hex) {
    vector<uint8_t> bytes;
    for (size_t i = 0; i < hex.length(); i += 2) {
        string byte_string = hex.substr(i, 2);
        uint8_t byte = (uint8_t)strtol(byte_string.c_str(), nullptr, 16);
        bytes.push_back(byte);
    }
    return bytes;
}

//**********************************************************************************
//**********************************************************************************
//                  XOR the byte vector with a single character key
//**********************************************************************************
//**********************************************************************************
string xor_with_key(const vector<uint8_t>& bytes, char key) {
    string result;
    for (auto byte : bytes) {
        result += (char)(byte ^ key);
    }
    return result;
}

//**********************************************************************************
//**********************************************************************************
//              Score the plaintext based on English letter frequencies
//**********************************************************************************
//**********************************************************************************
double score_text(const std::string& text) {
    double score = 0;
    for (char c : text) {
        c = tolower(c);
        if (english_frequencies.find(c) != english_frequencies.end()) {
            score += english_frequencies[c];
        }
    }
    return score;
}

string tester() {
    string hex_input;

    cout << "Enter the cipher: ";
    cin >> hex_input;
    vector<uint8_t> bytes = hex_to_bytes(hex_input);

    double best_score = -1;
    string best_plaintext;
    char best_key;

    // Try every possible single character (byte) as the XOR key
    for (int key = 0; key < 256; ++key) {
        string decrypted = xor_with_key(bytes, (char)key);
        double score = score_text(decrypted);

        // Keep track of the highest-scoring (best) result
        if (score > best_score) {
            best_score = score;
            best_plaintext = decrypted;
            best_key = key;
        }
    }

    // Output the best result
    cout << "Best key: " << hex << setw(2) << setfill('0') << (int)best_key << endl;
    cout << "Decrypted message: " << best_plaintext << endl;

    return best_plaintext;
}



*/


















/*

//Challenge 2

#include <iostream>
#include <string>
#include <bitset>
#include <sstream>
using namespace std;

// Fixed key for challenge
string key1 = "686974207468652062756c6c277320657965";

//****************************************************************************************
//              Function to convert a hex string to binary string
//****************************************************************************************
string hexToBinary(const string& hex) {
    string binary;
    for (size_t i = 0; i < hex.length(); ++i) {
        binary += bitset<4>(stoi(hex.substr(i, 1), nullptr, 16)).to_string();
    }
    return binary;
}

//****************************************************************************************
//              Function to convert binary string to hex string
//****************************************************************************************
string binaryToHex(const string& binary) {
    stringstream hexStream;
    for (size_t i = 0; i < binary.length(); i += 4) {
        hexStream << hex << stoi(binary.substr(i, 4), nullptr, 2);
    }
    return hexStream.str();
}

//****************************************************************************************
//              Function to XOR two binary strings of equal length
//****************************************************************************************
string toXOR(const string& bin1, const string& bin2) {
    string xorResult;
    for (size_t i = 0; i < bin1.length(); ++i) {
        xorResult += (bin1[i] == bin2[i]) ? '0' : '1';  // XOR operation
    }
    return xorResult;
}

//****************************************************************************************
//                              MAIN FUNCTION
//****************************************************************************************
string tester() {
    string hexInput;
    cout << "Enter the required input: ";
    cin >> hexInput;

    // Convert hex strings to binary
    string bin1 = hexToBinary(hexInput);
    string keyBin = hexToBinary(key1);

    // XOR the two binary strings
    string xorOutputBin = toXOR(bin1, keyBin);

    // Convert XORed binary result back to hex
    string xorOutputHex = binaryToHex(xorOutputBin);

    cout << "XOR result (hex): " << xorOutputHex << endl;

    return xorOutputHex;
}

*/


// Programmer: Muhammad Asjad Rehman Hashmi
// Program: Challenge 1 set 1

#include <iostream>
#include <string>
#include <bitset>
#include <sstream>

using namespace std;


// Base64 characters table
const string base64_chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" "abcdefghijklmnopqrstuvwxyz" "0123456789+/";

// Function to convert a hex string to binary string
    string hexToBinary(const string& hex) {
    string binary;

    // Convert each hex digit to a 4-bit binary string
    for (size_t i = 0; i < hex.length(); ++i) {
        binary += bitset<4>(stoi(hex.substr(i, 1), nullptr, 16)).to_string();
    }
    return binary;
}

// Function to convert a binary string to Base64 string
string binaryToBase64(const string& binary) {
    string base64;
    int i = 0;

    // Process 6-bit chunks of the binary string
    while (i + 6 <= binary.length()) {
        // Convert each 6-bit chunk to an integer, then to a Base64 character
        int value = bitset<6>(binary.substr(i, 6)).to_ulong();
        base64 += base64_chars[value];
        i += 6;
    }

    // Handle remaining bits
    int remaining_bits = binary.length() - i;
    if (remaining_bits > 0) {
        bitset<6> last_chunk(binary.substr(i, remaining_bits));
        base64 += base64_chars[last_chunk.to_ulong()];

        // Add padding based on how many bits were remaining
        base64 += string((6 - remaining_bits) / 2, '=');
    }

    return base64;
}

// Function to convert hex to Base64
string hexToBase64(const std::string& hex) {
    string binary = hexToBinary(hex);
    return binaryToBase64(binary);
}

string tester(string input) {
    string hexInput;

    cout << "Enter hexadecimal string: ";
    cin >> hexInput;

    string base64Output = hexToBase64(hexInput);

    cout << "Base64 encoded string: " << base64Output << endl;

    return base64Output;
}
