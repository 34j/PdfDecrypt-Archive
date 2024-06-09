# PdfDecrypt
 
# Moved to [34j/decryptpdf](https://github.com/34j/decryptpdf) (Python CLI Package) as pikepdf may be able to decrypt more types of PDF files.

## Requirements
win-x64, win-x86, win-arm, win-arm64, osx-x64, linux-x64, linux-arm

## Installation
1. Download the latest release from [Releases](https://github.com/34j/PdfDecrypt/releases).
2. Add the folder where the program is located to the environment variables.

## Usage
```console
> pdfdecrypt from
Password: ********
```
or
```console
> pdfdecrypt from.pdf -p password
```
`from.decrypted.pdf` will be generated.

```console
> pdfdecrypt -h
Usage: PdfDecrypt [options] <FromPath> <ToPath>

Arguments:
  FromPath                  The path of the file to remove the password from.
  ToPath                    The path to save the decrypted file to. (Optional)

Options:
  -p|--password <PASSWORD>  The password to use to decrypt the file.
  -na|--noaddextension      Whether not to add ".pdf" to FromPath or ToPath if it does not have extension.
  -?|-h|--help              Show help information.
```


