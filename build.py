import os
import getopt
import sys
import socket

#RENEW PFX
opts, args = getopt.getopt(sys.argv[1:], '',  ["renew-certificate", "help"]);
#os.system('docker build -t docker.data.techcollege.dk/wiseguy_pwa ./pwa');

def renew_certificates():
	print "Renewing certificate from let's encrypt certbot with elevated privileges"
	os.system("sudo certbot certonly --standalone")
	

for o, a in opts:
	if o == "--help":
		print """Usage: ./build [--renew-certificate]""";
		
	if o == "--renew-certificate":
		renew_certificates()



#BUILD IMAGES
#build pwa
#os.system('docker build -t docker.data.techcollege.dk/wiseguy_pwa ./pwa');
#build api
#os.system('docker build -t docker.data.techcollege.dk/wiseguy_api ./api');

#PUSH IMAGES TO REGISTRY
#os.system('docker push docker.data.techcollege.dk/wiseguy_pwa');
#os.system('docker push docker.data.techcollege.dk/wiseguy_api');
