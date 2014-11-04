@ECHO OFF

ECHO Extracting data keystore...
ECHO ...

SET pathKeyStore="C:\Projects\Repositories\toArray\FlappyAndFire\_build\Android\FlappyAndFire.keystore"
SET aliasKeyStore="FlappyAndFire"
SET storePass=key@android
SET keyPass=key@android
SET javaPath=C:\Program Files (x86)\Java\jre1.8.0_25\bin

CD %javaPath%

KEYTOOL -list -v -keystore %pathKeyStore% -alias %aliasKeyStore% -storepass %storePass% -keypass %keyPass%

ECHO ....
ECHO Completed extracting data keystores.

PAUSE