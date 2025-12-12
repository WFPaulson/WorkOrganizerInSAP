namespace Servicelibraries.Exceptions;
public class FileLocationNotFoundException : Exception {

    public FileLocationNotFoundException() { }

    public FileLocationNotFoundException(string message) 
        : base(message) { }

    public FileLocationNotFoundException(string message, Exception innerException) 
        : base(message, innerException) { }
    
        
}


