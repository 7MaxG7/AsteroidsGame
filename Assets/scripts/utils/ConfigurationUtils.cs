using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigurationUtils {
    static ConfigurationData config;

    #region Properties
    public static float ShipThrustForce {
        get { return config.ShipThrustForce; }
    }
    /// <summary>
    /// Rotation speed for inputs in degrees per second
    /// </summary>
    public static float ShipRotationSpeed {
        get { return config.ShipRotationSpeed; }
    }
    public static float BulletLifeTime {
        get { return config.BulletLifeTime; }
    }
    /// <summary>
    /// Start asteroids' spawn time in seconds
    /// </summary>
    public static float AsteroidsSpawnTime {
        get { return config.AsteroidsSpawnTime; }
    }
    /// <summary>
    /// Posible deviation of asteroid's movement from straight direction after spawning
    /// </summary>
    public static float AsteroidAngleMoveDeviation {
        get { return config.AsteroidAngleMoveDeviation; }
    }
    /// <summary>
    /// Minimum start asteroids' magnitude
    /// </summary>
    public static float MinAsteroidMagnitude {
        get { return config.MinAsteroidMagnitude; }
    }
    /// <summary>
    /// Maximum start asteroids' magnitude
    /// </summary>
    public static float MaxAsteroidMagnitude {
        get { return config.MaxAsteroidMagnitude; }
    }
    public static float DifficultyChangeTime {
        get { return config.DifficultyChangeTime; }
    }
    /// <summary>
    /// Amount of asteroids' spawn time decrease after difficulty increase
    /// </summary>
    public static float DifficultySpawnTimeMultiplicator {
        get { return config.DifficultySpawnTimeMultiplicator; }
    }
    /// <summary>
    /// Amount of asteroids' after spawn speed increase after difficulty increase
    /// </summary>
    public static float DifficultyMagnitudeMultiplicator {
        get { return config.DifficultyMagnitudeMultiplicator; }
    }

    #endregion

    public static void Initialize() {
        config = new ConfigurationData();
    }
}
