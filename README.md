# PdfDecrypt
## Requirements
Windows 7 or later

## Installation
1. Download the latest release from [Releases](https://github.com/34j/PdfDecrypt/releases).
2. Add the folder where the program is located to the environment variables.

## Usage
```console
> pdfdecrypt from.pdf
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
  ToPath (Optional)         The path to save the decrypted file to.

Options:
  -p|--password <PASSWORD>  The password to use to decrypt the file.
  -nc|--noclose             Whether not to close the application after decrypting the file.
  -?|-h|--help              Show help information.
```
