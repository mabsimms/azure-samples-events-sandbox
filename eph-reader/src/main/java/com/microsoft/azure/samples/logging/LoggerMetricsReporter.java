package com.microsoft.azure.samples.logging;

/**
 * Created by masimms on 9/13/17.
 */

import com.codahale.metrics.*;

import java.util.SortedMap;
import java.util.concurrent.TimeUnit;

public class LoggerMetricsReporter extends ScheduledReporter {

    private final Clock clock;

    public LoggerMetricsReporter(MetricRegistry registry, Clock clock, MetricFilter filter,
                                 TimeUnit rateUnit, TimeUnit durationUnit)
    {
        super(registry, "log-reporter", filter, rateUnit, durationUnit);
        this.clock = clock;
    }

    @Override
    public void report(SortedMap<String, Gauge> sortedMap, SortedMap<String, Counter> sortedMap1,
                       SortedMap<String, Histogram> sortedMap2, SortedMap<String, Meter> sortedMap3,
                       SortedMap<String, Timer> sortedMap4)
    {

    }
}

/*
public class DbReporter extends ScheduledReporter {
    private final Connection connection;
    private final Clock clock;

    public static DbReporter.Builder forRegistry(MetricRegistry registry) {
        return new DbReporter.Builder(registry);
    }

    private DbReporter(MetricRegistry registry, Connection connection, Clock clock, MetricFilter filter, TimeUnit rateUnit, TimeUnit durationUnit) {
        super(registry, "db-reporter", filter, rateUnit, durationUnit);
        this.connection = connection;
        this.clock = clock;
    }

    //...

    public static class Builder {
        private final MetricRegistry registry;
        private Connection connection;
        private TimeUnit rateUnit;
        private TimeUnit durationUnit;
        private MetricFilter filter;
        private Clock clock;

        private Builder(MetricRegistry registry) {
            this.registry = registry;
            this.connection = null;
            this.rateUnit = TimeUnit.SECONDS;
            this.durationUnit = TimeUnit.MILLISECONDS;
            this.filter = MetricFilter.ALL;
            this.clock = Clock.defaultClock();
        }

        public DbReporter.Builder outputTo(Connection connection) {
            this.connection = connection;
            return this;
        }

        public DbReporter.Builder convertRatesTo(TimeUnit rateUnit) {
            this.rateUnit = rateUnit;
            return this;
        }

        public DbReporter.Builder convertDurationsTo(TimeUnit durationUnit) {
            this.durationUnit = durationUnit;
            return this;
        }

        public DbReporter.Builder filter(MetricFilter filter) {
            this.filter = filter;
            return this;
        }

        public DbReporter build() {
            return new DbReporter(this.registry, this.connection, this.clock, this.filter, this.rateUnit, this.durationUnit);
        }
    }
}
 */
