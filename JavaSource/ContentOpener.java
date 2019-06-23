package com.cartoontexas.andyr.unityplugin;

import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.ProviderInfo;
import android.net.Uri;
import android.util.Log;
import android.webkit.MimeTypeMap;

import java.io.File;
import java.util.Locale;

/**
 * Created by yasirkula on 22.06.2017.
 * Modified by AndyRogerKats on 22.06.2019
 */

public class ContentOpener
{
    private static String authority = null;
    public static void OpenContent( Context context, String file)
    {
        if( GetAuthority( context ) == null )
        {
            Log.e( "Unity", "Can't find ContentProvider, open not possible!" );
            return;
        }

        Intent intent = new Intent();
        int extensionStart = file.lastIndexOf( '.' );
        // Credit: https://stackoverflow.com/a/31691791/2373034
        String thisMime = MimeTypeMap.getSingleton().getMimeTypeFromExtension( file.substring( extensionStart + 1 ).toLowerCase( Locale.ENGLISH ) );
        Uri contentUri = UnitySSContentProvider.getUriForFile( context, authority, new File( file ) );
        intent.setAction(Intent.ACTION_VIEW);
        intent.setDataAndType(contentUri, thisMime);
        intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
        context.startActivity(intent);
        return;
    }

    private static String GetAuthority( Context context )
    {
        if( authority == null )
        {
            // Find the authority of ContentProvider first
            // Credit: https://stackoverflow.com/a/2001769/2373034
            try
            {
                PackageInfo packageInfo = context.getPackageManager().getPackageInfo( context.getPackageName(), PackageManager.GET_PROVIDERS );
                ProviderInfo[] providers = packageInfo.providers;
                if( providers != null )
                {
                    for( ProviderInfo provider : providers )
                    {
                        if( provider.name.equals( UnitySSContentProvider.class.getName() ) && provider.packageName.equals( context.getPackageName() )
                                && provider.authority.length() > 0 )
                        {
                            authority = provider.authority;
                            break;
                        }
                    }
                }
            }
            catch( Exception e )
            {
                Log.e( "Unity", "Exception:", e );
            }
        }

        return authority;
    }
}
