
import subprocess
import os

def run_build():
    try:
        # Run dotnet build and capture output
        process = subprocess.Popen(['dotnet', 'build'], stdout=subprocess.PIPE, stderr=subprocess.PIPE)
        stdout, stderr = process.communicate()
        
        # Decode - try utf-8 then utf-16 or cp1252
        try:
            out_str = stdout.decode('utf-8')
        except:
            try:
                out_str = stdout.decode('cp1252')
            except:
                out_str = stdout.decode('utf-16', errors='ignore')
                
        lines = out_str.splitlines()
        errors = [line for line in lines if ": error" in line]
        
        with open("error_analysis.txt", "w", encoding="utf-8") as f:
            f.write(f"Total Errors Found: {len(errors)}\n")
            for i, err in enumerate(errors[:5]):
                f.write(f"Error {i+1}: {err.strip()}\n")
        print("Analysis written to error_analysis.txt")
            
    except Exception as e:
        print(f"Script Error: {e}")

if __name__ == "__main__":
    run_build()
