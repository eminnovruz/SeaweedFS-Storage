# Quick Start #

## Quick Start for S3 API on Docker ##

`docker run -p 8333:8333 chrislusf/seaweedfs server -s3`

## Quick Start with Single Binary ##
* Download the latest binary from https://github.com/seaweedfs/seaweedfs/releases and unzip a single binary file `weed` or `weed.exe`. Or run `go install github.com/seaweedfs/seaweedfs/weed@latest`.
* Run `weed server -dir=/some/data/dir -s3` to start one master, one volume server, one filer, and one S3 gateway.

# Introduction #

SeaweedFS is a simple and highly scalable distributed file system. There are two objectives:

1. to store billions of files!
2. to serve the files fast!

SeaweedFS started as an Object Store to handle small files efficiently. 
Instead of managing all file metadata in a central master, 
the central master only manages volumes on volume servers, 
and these volume servers manage files and their metadata. 
This relieves concurrency pressure from the central master and spreads file metadata into volume servers, 
allowing faster file access (O(1), usually just one disk read operation).

There is only 40 bytes of disk storage overhead for each file's metadata. 
It is so simple with O(1) disk reads that you are welcome to challenge the performance with your actual use cases.

## Example: Using Seaweed Object Store ##

By default, the master node runs on port 9333, and the volume nodes run on port 8080.
Let's start one master node, and two volume nodes on port 8080 and 8081. Ideally, they should be started from different machines. We'll use localhost as an example.

SeaweedFS uses HTTP REST operations to read, write, and delete. The responses are in JSON or JSONP format.

### Start Master Server ###

```
> ./weed master
```

### Start Volume Servers ###

```
> weed volume -dir="/tmp/data1" -max=5  -mserver="localhost:9333" -port=8080 &
> weed volume -dir="/tmp/data2" -max=10 -mserver="localhost:9333" -port=8081 &
```

### Write File ###

To upload a file: first, send a HTTP POST, PUT, or GET request to `/dir/assign` to get an `fid` and a volume server URL:

```
> curl http://localhost:9333/dir/assign
{"count":1,"fid":"3,01637037d6","url":"127.0.0.1:8080","publicUrl":"localhost:8080"}
```

Second, to store the file content, send a HTTP multi-part POST request to `url + '/' + fid` from the response:

```
> curl -F file=@/home/chris/myphoto.jpg http://127.0.0.1:8080/3,01637037d6
{"name":"myphoto.jpg","size":43234,"eTag":"1cc0118e"}
```

To update, send another POST request with updated file content.

For deletion, send an HTTP DELETE request to the same `url + '/' + fid` URL:

```
> curl -X DELETE http://127.0.0.1:8080/3,01637037d6
```

### Save File Id ###

Now, you can save the `fid`, 3,01637037d6 in this case, to a database field.

The number 3 at the start represents a volume id. After the comma, it's one file key, 01, and a file cookie, 637037d6.

The volume id is an unsigned 32-bit integer. The file key is an unsigned 64-bit integer. The file cookie is an unsigned 32-bit integer, used to prevent URL guessing.

The file key and file cookie are both coded in hex. You can store the <volume id, file key, file cookie> tuple in your own format, or simply store the `fid` as a string.

If stored as a string, in theory, you would need 8+1+16+8=33 bytes. A char(33) would be enough, if not more than enough, since most uses will not need 2^32 volumes.

If space is really a concern, you can store the file id in your own format. You would need one 4-byte integer for volume id, 8-byte long number for file key, and a 4-byte integer for the file cookie. So 16 bytes are more than enough.

### Read File ###

Here is an example of how to render the URL.

First look up the volume server's URLs by the file's volumeId:

```
> curl http://localhost:9333/dir/lookup?volumeId=3
{"volumeId":"3","locations":[{"publicUrl":"localhost:8080","url":"localhost:8080"}]}
```

Since (usually) there are not too many volume servers, and volumes don't move often, you can cache the results most of the time. Depending on the replication type, one volume can have multiple replica locations. Just randomly pick one location to read.

Now you can take the public URL, render the URL or directly read from the volume server via URL:

```
 http://localhost:8080/3,01637037d6.jpg
```

Notice we add a file extension ".jpg" here. It's optional and just one way for the client to specify the file content type.

If you want a nicer URL, you can use one of these alternative URL formats:

```
 http://localhost:8080/3/01637037d6/my_preferred_name.jpg
 http://localhost:8080/3/01637037d6.jpg
 http://localhost:8080/3,01637037d6.jpg
 http://localhost:8080/3/01637037d6
 http://localhost:8080/3,01637037d6
```
